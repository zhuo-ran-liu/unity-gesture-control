using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;

public class GestureRotationTracker : MonoBehaviour
{
    UdpClient udpClient;
    Thread receiveThread;
    public int port = 5050;

    private Queue<Vector3> positionQueue = new Queue<Vector3>();
    private Vector3 lastScreenPos = Vector3.zero;

    [Header("Enable Rotation")]
    public bool toggleX = true;
    public bool toggleY = true;

    [Header("Invert Movement Directions")]
    public bool invertX = false;  // ← Add this
    public bool invertY = false;  // ← And this

    [Header("Rotation Range")]
    public float rotationSensitivity = 180f;
    public float smoothSpeed = 5f;

    private float movementThreshold = 1f;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation;

        udpClient = new UdpClient(port);
        receiveThread = new Thread(ReceiveData);
        receiveThread.IsBackground = true;
        receiveThread.Start();
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
                    Vector3 screenPos = new Vector3(x, y, z);

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
        ApplyRotationFromScreenInput();
    }

    void ApplyRotationFromScreenInput()
    {
        if (positionQueue.Count > 0)
        {
            Vector3 screenPos;
            lock (positionQueue)
            {
                screenPos = positionQueue.Dequeue();
            }

            float movement = Vector2.Distance(
                new Vector2(screenPos.x, screenPos.y),
                new Vector2(lastScreenPos.x, lastScreenPos.y)
            );

            if (movement < movementThreshold)
                return;

            lastScreenPos = screenPos;

            float normX = ((screenPos.x / 1280f) - 0.5f) * 2f;
            float normY = ((screenPos.y / 720f) - 0.5f) * 2f;

            // Apply inversion if toggled
            if (invertX) normX = -normX;
            if (invertY) normY = -normY;

            float rotX = toggleX ? normY * rotationSensitivity : 0f;
            float rotY = toggleY ? normX * rotationSensitivity : 0f;

            targetRotation = Quaternion.Euler(rotX, rotY, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothSpeed);

            Vector3 euler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(euler.x, euler.y, 0f);
        }
    }

    void OnApplicationQuit()
    {
        if (udpClient != null) udpClient.Close();
        if (receiveThread != null && receiveThread.IsAlive) receiveThread.Abort();
    }
}
