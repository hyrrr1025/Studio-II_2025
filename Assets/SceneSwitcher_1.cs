using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher_1 : MonoBehaviour
{
    [Header("目标物体")]
    public GameObject targetObject;  // 需要监控的物体

    [Header("时间设置")]
    public float requiredActiveTime = 5f; // 物体需要激活的时间（秒）

    [Header("输入设置")]
    public KeyCode switchKey = KeyCode.DownArrow; // 触发切换的按键

    private float activeTimer = 0f;
    private bool isTimerActive = false;

    void Update()
    {
        // 检查目标物体是否激活
        if (targetObject != null && targetObject.activeInHierarchy)
        {
            // 启动/继续计时
            activeTimer += Time.deltaTime;
            isTimerActive = true;

            Debug.Log($"物体已激活: {activeTimer.ToString("F1")}/{requiredActiveTime}秒");

            // 检查是否达到要求时间
            if (activeTimer >= requiredActiveTime)
            {
                Debug.Log("等待按下切换键...");

                // 检测按键输入
                if (Input.GetKeyDown(switchKey))
                {
                    SwitchScene();
                }
            }
        }
        else if (isTimerActive)
        {
            // 物体变为非激活状态时重置计时器
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        activeTimer = 0f;
        isTimerActive = false;
        Debug.Log("计时器已重置");
    }

    void SwitchScene()
    {
        // 获取当前场景索引
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 计算下一个场景索引（循环切换）
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // 加载新场景
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log($"切换到场景: {nextSceneIndex}");
    }

    // 可选：在物体激活时自动开始计时
    void OnEnable()
    {
        if (targetObject == this.gameObject)
        {
            ResetTimer();
        }
    }
}
