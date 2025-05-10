using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class GestureScaleTracker : MonoBehaviour
{
    private Thread receiveThread;
    private UdpClient client;
    public int port = 5050;
    public bool printToConsole = true;

    private volatile float scaleFactor = 1.0f;

    [Header("Intensity")]
    public float intensity = 0.1f;

    [Header("ScaleX")]
    public bool scaleX = true;

    [Header("ScaleY")]
    public bool scaleY = true;

    [Header("ScaleZ")]
    public bool scaleZ = true;

    [Header("Smoothing")]
    public float smoothSpeed = 5f;

    private Vector3 targetScale;

    void Start()
    {
        // Start UDP receive thread
        receiveThread = new Thread(ReceiveData) { IsBackground = true };
        receiveThread.Start();

        // Set initial scale based on scaleFactor and intensity
        float initialScale = scaleFactor * intensity;
        Vector3 initScale = transform.localScale;

        transform.localScale = new Vector3(
            scaleX ? initialScale : initScale.x,
            scaleY ? initialScale : initScale.y,
            scaleZ ? initialScale : initScale.z
        );
    }

    void Update()
    {
        ApplyScale();
    }

    void ApplyScale()
    {
        float desiredScale = scaleFactor * intensity;
        Vector3 currentScale = transform.localScale;

        targetScale = new Vector3(
            scaleX ? desiredScale : currentScale.x,
            scaleY ? desiredScale : currentScale.y,
            scaleZ ? desiredScale : currentScale.z
        );

        // Smoothly interpolate scale
        transform.localScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime * smoothSpeed);
    }

    void ReceiveData()
    {
        client = new UdpClient(port);
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, port);

        while (true)
        {
            try
            {
                byte[] data = client.Receive(ref remoteEP);
                string message = Encoding.ASCII.GetString(data);

                if (printToConsole)
                {
                    Debug.Log("Received: " + message);
                }

                string[] values = message.Split(',');

                if (values.Length == 4 &&
                    float.TryParse(values[0], out float dist) &&
                    float.TryParse(values[1], out float x) &&
                    float.TryParse(values[2], out float y) &&
                    float.TryParse(values[3], out float z))
                {
                    scaleFactor = dist;
                }
            }
            catch (Exception e)
            {
                Debug.Log("UDP Receive Error: " + e.Message);
            }
        }
    }

    void OnApplicationQuit()
    {
        // Safely stop thread and close UDP client
        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }

        if (client != null)
        {
            client.Close();
        }
    }
}
