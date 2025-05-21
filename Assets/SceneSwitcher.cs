using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string nextSceneName;

    public float delayTime = 5f;  // 新增可配置字段

    private bool canSwitch = false;

    void Start()
    {
        // 使用可配置的delayTime代替固定5秒
        Invoke("EnableSceneSwitch", delayTime);
    }

    void Update()
    {
        if (canSwitch && Input.GetKeyDown(KeyCode.DownArrow))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void EnableSceneSwitch()
    {
        canSwitch = true;
    }
}