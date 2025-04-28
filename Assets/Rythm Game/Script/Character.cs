using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [System.Serializable]
    public class SpriteTiming
    {
        public float triggerTime;
        public Sprite targetSprite;
    }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<SpriteTiming> spriteTimings = new List<SpriteTiming>();
    
    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // 按时间排序确保顺序正确
        spriteTimings.Sort((a, b) => a.triggerTime.CompareTo(b.triggerTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying || currentIndex >= spriteTimings.Count)
            return;

        float currentTime = audioSource.time;
        
        // 检查是否到达下一个触发时间（带0.1秒容差）
        if (currentTime >= spriteTimings[currentIndex].triggerTime - 0.1f)
        {
            spriteRenderer.sprite = spriteTimings[currentIndex].targetSprite;
            currentIndex++;
            
            // 可选：添加切换效果
            StartCoroutine(SwitchEffect());
        }
    }

    // 重置状态（当重新播放音乐时调用）
    public void ResetState()
    {
        currentIndex = 0;
    }

    // 切换特效示例
    System.Collections.IEnumerator SwitchEffect()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale *= 1.1f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;
    }
}
