using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;

public class GesturePositionTracker : MonoBehaviour
{
    UdpClient udpClient;
    Thread receiveThread;
    public int port = 5050;

    private Queue<Vector3> positionQueue = new Queue<Vector3>();
    private float currentZDepth;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    [Header("Enable Position Tracking")]
    [Tooltip("Toggle to apply X-axis transformation")]
    public bool toggleX = true;

    [Tooltip("Toggle to apply Y-axis transformation")]
    public bool toggleY = true;

    private float smoothSpeed = 5f;

    void Start()
    {
        // Store the object's initial position
        initialPosition = transform.position;
        targetPosition = initialPosition;

        // Get current screen Z-depth
        currentZDepth = Camera.main.WorldToScreenPoint(transform.position).z;

        // Start UDP listener
        udpClient = new UdpClient(port);
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();

        // Reset position on game start
        transform.position = initialPosition;
    }

    void ReceiveData()
    {
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, port);
        while (true)
        {
            try
            {
                byte[] data = udpClient.Receive(ref remoteEP);
                string message = Encoding.ASCII.GetString(data);
                string[] values = message.Split(',');

                if (values.Length == 4 &&
                    float.TryParse(values[1], out float x) &&
                    float.TryParse(values[2], out float y) &&
                    float.TryParse(values[3], out float z))
                {
                    Vector3 screenPos = new Vector3(x, y, 0);

                    lock (positionQueue)
                    {
                        positionQueue.Enqueue(screenPos);
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.Log("UDP Receive Error: " + e.Message);
            }
        }
    }

    void Update()
    {
        followExactPosition();
    }

    void followDirection(){

    }

    void followExactPosition(){
        if (positionQueue.Count > 0)
        {
            Vector3 screenPos;
            lock (positionQueue)
            {
                screenPos = positionQueue.Dequeue();
            }

            float x = screenPos.x;
            float y = screenPos.y;

            if (toggleX)
            {
                x = (screenPos.x / 1280f) * Screen.width;
            }

            if (toggleY)
            {
                y = Screen.height - (screenPos.y / 720f) * Screen.height;
            }

            Vector3 correctedScreenPos = new Vector3(x, y, currentZDepth);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(correctedScreenPos);

            Vector3 newTarget = transform.position;
            if (toggleX) newTarget.x = worldPos.x;
            if (toggleY) newTarget.y = worldPos.y;

            targetPosition = newTarget;
        }

        // Smooth movement
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
    }

    void OnApplicationQuit()
    {
        if (udpClient != null) udpClient.Close();
        if (receiveThread != null && receiveThread.IsAlive) receiveThread.Abort();
    }
}
