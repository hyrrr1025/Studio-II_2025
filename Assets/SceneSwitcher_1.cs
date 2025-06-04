using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher_1 : MonoBehaviour
{
    [Header("Ŀ������")]
    public GameObject targetObject;  // ��Ҫ��ص�����

    [Header("ʱ������")]
    public float requiredActiveTime = 5f; // ������Ҫ�����ʱ�䣨�룩

    [Header("��������")]
    public KeyCode switchKey = KeyCode.DownArrow; // �����л��İ���

    private float activeTimer = 0f;
    private bool isTimerActive = false;

    void Update()
    {
        // ���Ŀ�������Ƿ񼤻�
        if (targetObject != null && targetObject.activeInHierarchy)
        {
            // ����/������ʱ
            activeTimer += Time.deltaTime;
            isTimerActive = true;

            Debug.Log($"�����Ѽ���: {activeTimer.ToString("F1")}/{requiredActiveTime}��");

            // ����Ƿ�ﵽҪ��ʱ��
            if (activeTimer >= requiredActiveTime)
            {
                Debug.Log("�ȴ������л���...");

                // ��ⰴ������
                if (Input.GetKeyDown(switchKey))
                {
                    SwitchScene();
                }
            }
        }
        else if (isTimerActive)
        {
            // �����Ϊ�Ǽ���״̬ʱ���ü�ʱ��
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        activeTimer = 0f;
        isTimerActive = false;
        Debug.Log("��ʱ��������");
    }

    void SwitchScene()
    {
        // ��ȡ��ǰ��������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ������һ������������ѭ���л���
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // �����³���
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log($"�л�������: {nextSceneIndex}");
    }

    // ��ѡ�������弤��ʱ�Զ���ʼ��ʱ
    void OnEnable()
    {
        if (targetObject == this.gameObject)
        {
            ResetTimer();
        }
    }
}
