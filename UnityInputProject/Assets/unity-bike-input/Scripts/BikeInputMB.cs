using System;
using System.IO.Ports;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BikeInputMB : MonoBehaviour
{
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

        SerialPort port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
        port.DataReceived += Port_DataReceived;
        port.Open();

        Debug.Log("port is open: " + port.IsOpen);
    }

    private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        string data = port.ReadExisting();

        Debug.Log(data);
    }


    // Update is called once per frame
    void Update()
    {
        string line = port.ReadLine();
        if (line != null && line != "")
        {
            Debug.Log(line);
        }
    }
}
