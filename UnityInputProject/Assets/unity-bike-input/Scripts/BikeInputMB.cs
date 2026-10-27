using System;
using System.Collections;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Ports;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class BikeInputMB : MonoBehaviour
{
    public UnityEvent<int> ValueUpdated;
    [SerializeField]
    private string[] ports;

    [SerializeField]
    private string portName;
    [SerializeField]
    private SerialPort port;

    private ConcurrentQueue<string> receivedMessages =
        new ConcurrentQueue<string>();

    private bool running;
    private Thread readThread;
    void Start()
    {
        // Get a list of serial port names.
        ports = SerialPort.GetPortNames();

        Debug.Log("The following serial ports were found:");

        // Display each port name to the console.
        foreach (string portName in ports)
        {
            Debug.Log(portName);
        }


        try
        {
            port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            port.Open();
            Debug.Log("port is open: " + port.IsOpen);

            running = true;
            readThread = new Thread(ReadLoop);
            readThread.IsBackground = true;
            readThread.Start();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to open serial port: {ex.Message}");
        }

    }

    private void ReadLoop()
    {
        while (running)
        {
            try
            {
                string line = port.ReadLine();
                receivedMessages.Enqueue(line);
            }
            catch (TimeoutException)
            {
                // Expected when no data arrives
            }
            catch (Exception ex)
            {
                Debug.LogError($"Serial read error: {ex.Message}");
                running = false;
            }
        }
    }

    void Update()
    {
        while (receivedMessages.TryDequeue(out string message))
        {
            Debug.Log($"Received: {message}");

            ValueUpdated?.Invoke(int.Parse(message));
        }
    }

    void OnDestroy()
    {
        running = false;

        if (readThread != null && readThread.IsAlive)
            readThread.Join();

        if (port != null && port.IsOpen)
            port.Close();
    }


}
