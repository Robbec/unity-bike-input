using System;
using System.IO.Ports;
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

        port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
        port.Open();

        Debug.Log("port is open: " + port.IsOpen);
    }



    // Update is called once per frame
    void Update()
    {
        string line = port.ReadLine();
        if (line != null && line != "")
        {
            Debug.Log(line);
            ValueUpdated?.Invoke(int.Parse(line));
        }
    }
}
