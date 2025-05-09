using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class KeyboardSimulator : MonoBehaviour
{
    public static KeyboardSimulator instance;
    private void Awake()
    {
        instance = this;
    }
    [DllImport("user32.dll", EntryPoint = "keybd_event")]
    static extern void keybd_event(
            byte bVk,            //虚拟键值 对应按键的ascll码十进制值  
            byte bScan,          //0
            int dwFlags,         //0 为按下，1按住，2为释放 
            int dwExtraInfo      //0
        );
    public  void SimulateKeyPress(byte keyCode)
    {
        keybd_event(66, 0, 0, 0);
        //keybd_event(66, 0, 1, 0);
        keybd_event(66, 0, 2, 0);
    }

    void Update()
    {
 
    }
}