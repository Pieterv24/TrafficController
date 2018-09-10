using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using Assets.Models;

public class Networking : MonoBehaviour
{
    [SerializeField]
    GameObject _Stopregeling;

    [SerializeField]
    GameObject _brug;

    private DateTime lastReconnectAttempt;
    public string IpAddress = "localhost";
    public int Port = 1234;
    public static string Test = "{\"type\":\"PrimaryTrigger\",\"triggered\":true,\"id\":\"1.1\"}";

    public string stop11 = "red";
    public string stop12 = "red";
    public string stop13 = "red";
    public string stop14 = "red";
    public string stop15 = "red";
    public string stop16 = "red";
    public string stop17 = "red";
    public string stop18 = "red";
    public string stop19 = "red";
    public string stop110 = "red";
    public string stop111 = "red";
    public string stop112 = "red";
    public string stop113 = "green";
    public string stopB1 = "red";
    public string stopB2 = "red";
    public string stopB3 = "red";
    public string stopB4 = "red";
    public string stopBo1 = "red";
    public string stopBo2 = "red";
    public bool brugmoetopen = false;


    TcpClient tcpClient = new TcpClient();
    NetworkStream serverStream;
    StreamWriter sw;
    StreamReader sr;

    private Thread writeThread;
    private Thread readThread;
    private bool runThread = true;
    private object streamMutex = new object();
    private object clientMutex = new object();

    private object queueMutex = new object();
    private Queue<string> outgoingDataQueue = new Queue<string>();

    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
        readThread = new Thread(SocketReadThread);
        writeThread = new Thread(SocketWriteThread);
        readThread.Start();
        writeThread.Start();
    }
    
    void Update()
    {

    }

    void FixedUpdate()
    {
        // Debug.Log("Hey");
    }

    void OnDestroy()
    {
        // tcpClient.Close();
        runThread = false;
        readThread.Join();
        writeThread.Join();
    }

    void Connect()
    {
        try
        {
            tcpClient.Connect(IpAddress, Port);
            serverStream = tcpClient.GetStream();
            sr = new StreamReader(serverStream);
            sw = new StreamWriter(serverStream);
            sw.AutoFlush = true;
            Debug.Log("Connected");
        }
        catch (SocketException e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            lastReconnectAttempt = DateTime.Now;
        }
    }

    public void addTrigger(Trigger trigger)
    {
        lock (queueMutex)
        {
            outgoingDataQueue.Enqueue(JsonUtility.ToJson(trigger));
        }
    }

    // Send signal to controller that the bridge is opened
    public void addBrigeResponse(BridgeResponse bridgeResponse)
    {
        lock (queueMutex)
        {
            try
            {
                Debug.Log(JsonUtility.ToJson(bridgeResponse));
                Debug.Log(bridgeResponse.type);
                outgoingDataQueue.Enqueue(JsonUtility.ToJson(bridgeResponse));
            } catch (Exception e)
            {
                Debug.Log("Error in bridgedata");
                Debug.Log(e.Message);
            }
        }
    }

    void SocketReadThread()
    {
        Debug.Log("Read Thread Started");
        while (runThread)
        {
            if (tcpClient.Connected)
            {
                string json = sr.ReadLine();
                bool formattingSucceeded = false;
                if (!formattingSucceeded)
                {
                    try
                    {
                        TraficLightData jsonObject = JsonUtility.FromJson<TraficLightData>(json);

                        if (jsonObject != null)
                        {
                            // Debug.Log(json);

                            try
                            {
                                foreach (LightInfo info in jsonObject.trafficLights)
                                {
                                    try
                                    {
                                        //Debug.Log(info.id);
                                        //Debug.Log(info.lightStatus);
                                        if (info.id == "1.1") //testing
                                            stop11 = info.lightStatus;
                                        if (info.id == "1.2") //testing
                                            stop12 = info.lightStatus;
                                        if (info.id == "1.3") //testing
                                            stop13 = info.lightStatus;
                                        if (info.id == "1.4") //testing
                                            stop14 = info.lightStatus;
                                        if (info.id == "1.5") //testing
                                            stop15 = info.lightStatus;
                                        if (info.id == "1.6") //testing
                                            stop16 = info.lightStatus;
                                        if (info.id == "1.7") //testing
                                            stop17 = info.lightStatus;
                                        if (info.id == "1.8") //testing
                                            stop18 = info.lightStatus;
                                        if (info.id == "1.9") //testing
                                            stop19 = info.lightStatus;
                                        if (info.id == "1.10") //testing
                                            stop110 = info.lightStatus;
                                        if (info.id == "1.11") //testing
                                            stop111 = info.lightStatus;
                                        if (info.id == "1.12") //testing
                                            stop112 = info.lightStatus;
                                        if (info.id == "1.13") //testing
                                            stop113 = info.lightStatus;
                                        if (info.id == "2.1") //testing
                                            stopB1 = info.lightStatus;
                                        if (info.id == "2.2") //testing
                                            stopB2 = info.lightStatus;
                                        if (info.id == "2.3") //testing
                                            stopB3 = info.lightStatus;
                                        if (info.id == "2.4") //testing
                                            stopB4 = info.lightStatus;
                                        if (info.id == "4.1") //testing
                                            stopBo1 = info.lightStatus;
                                        if (info.id == "4.2") //testing
                                            stopBo2 = info.lightStatus;
                                    }
                                    catch
                                    {
                                    }
                                }
                                formattingSucceeded = true;
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch
                    {
                    }
                }

                if (!formattingSucceeded)
                {
                    try
                    {
                        BridgeData bridgeData = JsonUtility.FromJson<BridgeData>(json);
                        if (bridgeData != null && brugmoetopen != bridgeData.bridgeOpen)
                        {
                            brugmoetopen = bridgeData.bridgeOpen;
                        }

                        formattingSucceeded = true;
                    }
                    catch
                    {
                    }
                }
                
            }
            else
            {
                lock (clientMutex)
                {
                    if (DateTime.Now > lastReconnectAttempt.AddSeconds(5))
                    {
                        Debug.Log("Reconnecting");
                        Connect();
                    }
                }
            }
        }
    }

    public void SocketWriteThread()
    {
        Debug.Log("Write Thread Started");
        while (runThread)
        {
            if (tcpClient.Connected)
            {
                try
                {
                    if (outgoingDataQueue.Count > 0)
                    {
                        lock (queueMutex)
                        {
                            Debug.Log(outgoingDataQueue.Peek());
                            sw.WriteLine(outgoingDataQueue.Dequeue());
                            //sw.WriteLine(Test);
                            //Debug.Log("trigger send");
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    throw e;
                }
            }
            else
            {
                lock (clientMutex)
                {
                    if (DateTime.Now > lastReconnectAttempt.AddSeconds(5))
                    {
                        Debug.Log("Reconnecting");
                        Connect();
                    }
                }
            }
            Thread.Sleep(200);
        }
    }
}
