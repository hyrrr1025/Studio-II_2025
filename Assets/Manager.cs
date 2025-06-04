using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public GameData gameData;
    Client_UI _Client_UI;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Manager.instance.gameData = new GameData();
        _Client_UI = new Client_UI();
    }
    private void OnApplicationQuit()
    {
        _Client_UI.OnApplicationQuit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class GameData
{
    public int aX;//aX(加速度计X轴数据)
    public int aY; //aY（加速度计Y轴数据）
    public int aZ; //aY（加速度计Z轴数据）
    public int gX; //gX（陀螺仪X轴数据）
    public int gY;//gX（陀螺仪Y轴数据）
    public int gZ;//gX（陀螺仪Z轴数据）
    public int light;//Light（对应哪个灯常亮）灯数据表示 0--红色   1--紫色   2--黄色   3--蓝色
}
