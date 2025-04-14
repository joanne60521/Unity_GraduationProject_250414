using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System;

public class Gyroscope : MonoBehaviour
{
    // port control
    string portName = "COM5";
    int baudRate = 115200;    // need to change the baudRate!
    Parity parity = Parity.None;
    int dataBits = 8;
    StopBits stopBits = StopBits.One;
    SerialPort sp5 = null;

    [SerializeField] string getypr;
    string[] prStr = new string[2]; // store the pitch roll string value
    string[] gyroStr1 = new string[2];
    string[] gyroStr2 = new string[2];
    public float[] prFlt1 = new float[2];   // store the yaw pitch roll float value 
    public float[] prFlt2 = new float[2];


    private Thread serialThread;
    private bool isRunning = false;
    private string latestData = "";

    void Start()
    {
        sp5 = new SerialPort(portName, baudRate, parity, dataBits,stopBits);
        sp5.ReadTimeout = 50;
        try{
            sp5.Open();
            Debug.Log("sp5 is opened");
            isRunning = true;
        }catch(IOException ex) 
        {
            Debug.Log(ex.Message);
        }


        serialThread = new Thread(ReadSerialData);
        serialThread.Start();
    }

    void ReadSerialData()
    {
        while (isRunning)
        {
            try
            {
                string data = sp5.ReadLine(); // 讀取完整一行
                lock (latestData) { latestData = data; } // 更新最新資料
            }
            catch (TimeoutException) { } // 忽略超時
        }
    }

    void Update()
    {
        ReadData();
    }

    public void ReadData() 
    {
        if(sp5.IsOpen)
        {
            lock (latestData)
            {
                if (!string.IsNullOrEmpty(latestData))
                {
                    getypr = latestData;
                    prStr = getypr.Split("|");     // need to split the string and store to ypystr array
                    // getypr = sp5.ReadExisting();   //get the arduino output
                    // Debug.Log("getypr = " + getypr);
                    if(prStr.Length == 2 && prStr[0] != "" && prStr[1] != "")
                    {
                        // Debug.Log("getypr: " + getypr);
                        // Debug.Log("prStr: " + prStr[0] + "  ,  " + prStr[1]);
                        gyroStr1 = prStr[0].Split(",");
                        gyroStr2 = prStr[1].Split(",");
                        if (gyroStr1.Length == 2 && gyroStr2.Length == 2 && gyroStr1[0] != "" && gyroStr2[1] != "")
                        {
                            for(int i=0; i < gyroStr1.Length; i++)
                            {
                                try{
                                    prFlt1[i] = float.Parse(gyroStr1[i]); // change string to float 
                                }catch (IOException ex) {
                                    Debug.LogWarning(ex.Message);
                                }
                            }
                            for(int i=0; i < gyroStr1.Length; i++)
                            {
                                try{
                                    prFlt2[i] = float.Parse(gyroStr2[i]); // change string to float 
                                }catch (IOException ex) {
                                    Debug.LogWarning(ex.Message);
                                }
                            }
                            // Debug.Log("prFlt1 = " + prFlt1[0] + " & " + prFlt1[1]);
                            // Debug.Log("prFlt2 = " + prFlt2[0] + " & " + prFlt2[1]);
                        }
                    }else{
                        // Debug.LogWarning("Length is not correct!");
                    }
                    latestData = ""; // 清空
                }
            }

        }
    }

    void OnApplicationQuit()
    {
        if(sp5 != null)
        {
            isRunning = false;
            serialThread.Join();
            sp5.Close();
        }
    }
}
