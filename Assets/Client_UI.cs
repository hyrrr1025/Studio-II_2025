using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.Threading;

public class Client_UI{
	
	private static byte[] data=new byte[1024];//用来接收和发送数据
	static string ips;
	static int rot;
	static bool isAuto = true;
	public Client_UI()
	{
		Debug.Log("tcp启动");
		ips = "192.168.43.1";
		rot = 16866;
		beginClient();
	}
	public  void OnApplicationQuit()
	{
		Debug.Log("OnApplicationQuit：");
		isAuto = false;
		closeConnected();
	}
	//void OnDestroy()
	//{
	//	Debug.Log("OnDestroy：");
	//	isAuto = false;
	//	closeConnected();
	//}
	
	public static  void SendMessageToServer(string Index)
	{
		if (sockClient == null || !sockClient.Connected)
			return;
		data =Encoding.UTF8.GetBytes(Index);
		sockClient.Send(data);
	}

	public static void SendMessageToServer(byte[] Index)
	{
		if (sockClient == null || !sockClient.Connected)
			return;
		sockClient.Send(Index);
	}
	static Socket sockClient = null;
	static bool Isconnection = false;
	static void connection()
	{
		IPAddress ip = null;
		IPEndPoint endPoint = null;
		ip = IPAddress.Parse(ips);
		endPoint = new IPEndPoint(ip, rot);
		while (isAuto)
		{
            if (Isconnection)
            {
                continue;
            }
            Isconnection = true;
			try
			{
				sockClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
#if UNITY_IOS
				sockClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
#endif
				sockClient.Connect(endPoint);
			}
			catch (Exception se)
			{
				Thread.Sleep(1000);
				Isconnection = false;
				closeConnected();
				//Debug.Log("连接异常1：" + se.Message);
			}
			if (Isconnection)
			{
				RecMsgthread = new Thread(RecMsg);
				RecMsgthread.IsBackground = true;
				RecMsgthread.Start();
				//LoadManager.instance.DoCoroutine(RecMsg());
				Debug.Log("连接成功：");
			}
		}
	}
	static Thread RecMsgthread;
	static Thread connectionthread;

	static void closeConnected()
    {
		//if (RecMsgthread!=null|| RecMsgthread.IsAlive)
		//	RecMsgthread.Abort();
		//if (connectionthread != null || connectionthread.IsAlive)
		//	connectionthread.Abort();

		if (sockClient != null)
			sockClient.Close();
		//LoadManager.instance.DoCoroutine(connection());
		//Debug.Log("断开连接");

	}
	void beginClient()
	{
		connectionthread = new Thread(connection);
		connectionthread.IsBackground = true;
		connectionthread.Start();
	}
	static void  RecMsg()
	{

		while (sockClient.Connected)
		{
			byte[] arrMsgRec = new byte[1024 * 1024 * 2];
			int length = -1;
			try
			{
				length = sockClient.Receive(arrMsgRec);
			}
			catch (Exception e)
			{
				length = -1;
				//Debug.Log("接收异常：" + e.Message);
				Isconnection = false;
				break;
			}
			if (length == -1 || length == 0)
			{
				Isconnection = false;
				//Debug.Log("接收中断");
				break;
			}
			string strMsg = System.Text.Encoding.UTF8.GetString(arrMsgRec, 0, length);// 将接受到的字节数据转化成字符串；
            string[] str = strMsg.Split(".");
            if (str.Length >=7)
			{
                string[] str2 = str[0].Split(":");
				Manager.instance.gameData.aX = int.Parse(str2[1]);
                str2 = str[1].Split(":");
                Manager.instance.gameData.aY = int.Parse(str2[1]);
                str2 = str[2].Split(":");
                Manager.instance.gameData.aZ = int.Parse(str2[1]);
                str2 = str[3].Split(":");
                Manager.instance.gameData.gX = int.Parse(str2[1]);
                str2 = str[4].Split(":");
                Manager.instance.gameData.gY = int.Parse(str2[1]);
                str2 = str[5].Split(":");
                Manager.instance.gameData.gZ = int.Parse(str2[1]);
                str2 = str[6].Split(":");
                Manager.instance.gameData.light = int.Parse(str2[1].Substring(0,1));
            }
        }
		Isconnection = false;
		closeConnected();
	}
}