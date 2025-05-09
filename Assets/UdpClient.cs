using UnityEngine;
using System.Collections;
//引入库
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
//using UnityEngine.tvOS;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using Baracuda.Threading;

public class UdpClient : MonoBehaviour
{
    public static UdpClient instance;
    private void Awake()
    {
        SceneName = SceneManager.GetActiveScene().name;
        instance = this;
    }
    string SceneName = "";
    string editString = "hello wolrd"; //编辑框文字

    //以下默认都是私有的成员
    Socket socket; //目标socket
    EndPoint serverEnd; //服务端
    IPEndPoint ipEnd; //服务端端口
    string recvStr; //接收的字符串
    string sendStr; //发送的字符串
    byte[] recvData = new byte[1024]; //接收的数据，必须为字节
    byte[] sendData = new byte[1024]; //发送的数据，必须为字节
    int recvLen; //接收的数据长度
    Thread connectThread; //连接线程
    int a;
    //初始化
    void InitSocket()
    {
        //定义连接的服务器ip和端口，可以是本机ip，局域网，互联网
        ipEnd = new IPEndPoint(IPAddress.Parse("192.168.10.254"), 8899);//
        //定义套接字类型,在主线程中定义
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
        //定义服务端
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 8899);
        serverEnd = (EndPoint)sender;
        socket.Bind(new IPEndPoint(IPAddress.Any, 8899));
        print("waiting for sending UDP dgram");
        //socket.Bind(ipEnd);
        //建立初始连接，这句非常重要，第一次连接初始化了serverEnd后面才能收到消息
        //SocketSend("hello");

        //开启一个线程连接，必须的，否则主线程卡死
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }

    void SocketSend(string sendStr)
    {
        //清空发送缓存
        sendData = new byte[1024];
        //数据类型转换
        sendData = Encoding.UTF8.GetBytes(sendStr);
        //发送给指定服务端
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, ipEnd);
    }

    //服务器接收
    void SocketReceive()
    {
        //进入接收循环
        while (true)
        {
            //对data清零
            recvData = new byte[1024];
            //获取客户端，获取服务端端数据，用引用给服务端赋值，实际上服务端已经定义好并不需要赋值
            recvLen = socket.ReceiveFrom(recvData, ref serverEnd);
            //输出接收到的数据
            try
            {
                recvStr = BitConverter.ToString(recvData, 0, recvLen).Replace("-", "");
                if (recvStr.Length == 28 && recvStr.Substring(0, 4) == "BBAA" && recvStr.Substring(24, 4) == "CCDD")
                {
                    string message = recvStr.Replace("BBAA", "").Replace("CCDD", "");
                    string _X = ReverString(message.Substring(0, 4));
                    _xX = ToInt32(_X);
                    if (SceneName == "Sokoban")
                    {
                        OnReshX(_xX);
                 
                    }
                    OnResh(_xX, ref OldIndeX);
                    //Debug.Log(string.Format("X:十六进制={0}-十进制={1}", _X, ToInt32(_X)));
                    string _Y = ReverString(message.Substring(4, 4));
                    _yY = ToInt32(_Y);
                    OnResh(_yY, ref OldIndeY);
                    //Debug.Log(string.Format("Y:十六进制={0}-十进制={1}", _Y, ToInt32(_Y)));
                    string _Z = ReverString(message.Substring(8, 4));
                    _zZ = ToInt32(_Z);
                    OnResh(_zZ, ref OldIndeZ);
                    //Debug.Log(string.Format("Z:十六进制={0}-十进制={1}", _Z, ToInt32(_Z)));
                    _Button = ReverString(message.Substring(12, 4));
                    //Debug.Log(string.Format("按钮:十六进制={0}-十进制={1}", _Button, Convert.ToInt32(_Button, 16)));
                    string Light = ReverString(message.Substring(16, 4));
                    _Light = Convert.ToInt32(Light, 16);
                    //Debug.Log(string.Format("灯光:十六进制={0}-十进制={1}", _Light, Convert.ToInt32(_Light, 16)));
                }
            }
            catch { }

            //a = Convert.ToInt32(recvStr,16);
        }
    }
    public int _xX;
    public int _yY;
    public int _zZ;
    public string _Button;
    public int _Light;

    public bool IsPlay = false;

    public int OldIndeX = -1;
    public int OldIndeY = -1;
    public int OldIndeZ = -1;
    bool IsWithinRange(double num, double target)
    {
        return Math.Abs(num - target) <= 5000;
    }
    public void OnReshX(int index)
    {
        if (OldIndeX == -1)
        {
            OldIndeX = index;
        }
        if (!IsWithinRange(index, OldIndeX))
        {
            if ((index - OldIndeX) > 0)
            {
                if (_Light % 2 == 0)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Player.instance.OnResh(1, 0);
                    });
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        Player.instance.OnResh(0, -1);
                    });
                  
                }
            }
            else
            {
                if (_Light % 2 == 0)
                {
                    Dispatcher.Invoke(() =>
                    {
                        Player.instance.OnResh(-1, 0);
                    });
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        Player.instance.OnResh(0, 1);
                    });
                }
            }
            OldIndeX = index;
            IsPlay = true;
        }
    }
    public void OnResh(int index,ref  int OldIndex)
    {
        if (OldIndex == -1)
        {
            OldIndex = index;
        }
        if (!IsWithinRange(index, OldIndex))
        {
            
            OldIndex = index;
            IsPlay = true;
        }
    }
    /// <summary>
    /// 字符串反转
    /// </summary>
    /// <param name="mess"></param>
    /// <returns></returns>
    public string ReverString(string mess)
    {
        var item = mess.Substring(0, 2);
        var item2 = mess.Substring(2, 2);
        return item2 + item;
    }
    public int ToInt32(string mess)
    { 
    return IfPositive(mess) ? Convert.ToInt32(mess, 16) : ToMinus(mess);
    }
    public bool IfPositive(string mess)
    {
        string hexString = mess;
        int decimalValue = Convert.ToInt32(hexString, 16); 
        string binaryString = Convert.ToString(decimalValue, 2); 
        string item = binaryString.Substring(binaryString.Length-2, 1);
        return item=="1" ? true : false;
    }
    public int ToMinus(string mess)
    {
        string hexString = mess;
        int decimalValue = Convert.ToInt32(hexString, 16);
        return (decimalValue);
    }
    //连接关闭
    void SocketQuit()
    {
        //关闭线程
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最后关闭socket
        if (socket != null)
            socket.Close();
    }

    // Use this for initialization
    void Start()
    {
        InitSocket(); //在这里初始化
    }

    float times = 0;
    // Update is called once per frame
    void Update()
    {
        if (IsPlay)
        {
            times += Time.deltaTime;
            if (times >= 0.3f)
            {
                times = 0;
                IsPlay = false;
            }
        }
    }

    void OnApplicationQuit()
    {
        SocketQuit();
    }


   

}