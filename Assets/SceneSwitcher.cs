using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string nextSceneName;

    public float delayTime = 5f;  // �����������ֶ�

    private bool canSwitch = false;

    void Start()
    {
        // ʹ�ÿ����õ�delayTime����̶�5��
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