using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    // Map.
    private MapCreater myMap;

    // Audio.
    private AudioSource audioSource;

    // 方向状态跟踪
    private bool moveTriggered = false;

    // 忽略传感器输入的帧数计数器
    private int ignoreSensorFrames = 0;
    private const int IGNORE_FRAME_COUNT = 55; // 忽略5帧

    private void Awake()
    {
        instance = this;
        myMap = FindObjectOfType<MapCreater>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        int dx = 0;
        int dy = 0;

        // 获取当前传感器值
        float currentGY = Manager.instance.gameData.gY;
        float currentGX = Manager.instance.gameData.gX;

        // 键盘输入处理
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dx--;
            // 键盘移动时也忽略后续传感器输入
            ignoreSensorFrames = IGNORE_FRAME_COUNT;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dx++;
            ignoreSensorFrames = IGNORE_FRAME_COUNT;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dy++;
            ignoreSensorFrames = IGNORE_FRAME_COUNT;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dy--;
            ignoreSensorFrames = IGNORE_FRAME_COUNT;
        }
        // 传感器输入处理 - 仅在移动未被触发且不在忽略帧时检测
        else if (!moveTriggered && ignoreSensorFrames <= 0)
        {
            // 左右移动检测（Z轴变化）
            if (currentGY <= -15500)
            {
                dx--;
                moveTriggered = true;
                // 检测到有效传感器输入，立即设置忽略帧
                ignoreSensorFrames = IGNORE_FRAME_COUNT;
            }
            else if (currentGY >= 15500)
            {
                dx++;
                moveTriggered = true;
                ignoreSensorFrames = IGNORE_FRAME_COUNT;
            }
            // 上下移动检测（Y轴变化）
            else if (currentGX <= -13000)
            {
                dy++;
                moveTriggered = true;
                ignoreSensorFrames = IGNORE_FRAME_COUNT;
            }
            else if (currentGX >= 13500)
            {
                dy--;
                moveTriggered = true;
                ignoreSensorFrames = IGNORE_FRAME_COUNT;
            }
        }
        else
        {
            // 当传感器值回到中性区域时重置移动触发
            if (currentGY < 2000 && currentGX < 2000)
            {
                moveTriggered = false;
            }
        }

        // 如果检测到移动输入
        if (dx != 0 || dy != 0)
        {
            // 处理移动
            HandleMovement(dx, dy);
        }

        // 更新忽略帧数计数器
        if (ignoreSensorFrames > 0)
        {
            ignoreSensorFrames--;
        }
    }
    void HandleMovement(int dx, int dy)
    {
        int nx = (int)transform.position.x + dx;
        int ny = (int)transform.position.y + dy;

        // 碰撞检测
        if (isWall(nx, ny)) return;

        // 处理箱子推动
        if (isBox(nx, ny))
        {
            int nnx = nx + dx;
            int nny = ny + dy;

            if (isWall(nnx, nny) || isBox(nnx, nny)) return;

            GameObject box = getBox(nx, ny);
            box.transform.position = new Vector3(nnx, nny);

            myMap.getPosBoxMap().Remove(myMap.TwoDToOneD(nx, ny));
            myMap.getPosBoxMap().Add(myMap.TwoDToOneD(nnx, nny), box);
        }

        // 执行移动
        transform.position = new Vector3(nx, ny);
        audioSource.Play();
        checkIfWin();
    }
    bool isWall(int x, int y)
    {
        return myMap.getWallPosSet().Contains(myMap.TwoDToOneD(x, y));
    }

    bool isBox(int x, int y)
    {
        return myMap.getPosBoxMap().ContainsKey(myMap.TwoDToOneD(x, y));
    }

    GameObject getBox(int x, int y)
    {
        return myMap.getPosBoxMap()[myMap.TwoDToOneD(x, y)];
    }

    void checkIfWin()
    {
        int num = 0;
        foreach (var tar_pos in myMap.getTargetPosList())
        {
            if (myMap.getPosBoxMap().ContainsKey(tar_pos)) ++num;
        }
        if (num == myMap.getTargetPosList().Count)
        {
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            sceneLoader.LoadNextScene();
        }
    }
}