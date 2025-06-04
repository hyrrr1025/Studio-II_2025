using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Basic Setting")]
    public AudioSource myMusic;

    public bool startPlaying;

    public BeatScroller BS;

    public static GameManager instance;

    public bool emergencyStop;

    //分数&倍率
    public float Score;
    public float ScorePerNote = 100;

    public TMP_Text scoreText;
    public TMP_Text multiplyText;

    public float multiply;
    public int multiplyTimes;
    public int baseNumber=1;

    [Header("ScoreValue Setting")]
    public float BaseScoreValue;
    public float PerfectScoreValue = 300;

    [Header("Result")]
    public float totalNotes;
    public float normalHits;
    public float perfectHits;
    public float missHits;

    public GameObject resultScreen;
    public TMP_Text normalCounter, perfectCounter, missedCounter, totalCounter;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        multiply = 1;

        // Note个数
        // totalNotes = FindObjectOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        multiplyText.text = "x" + multiply;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            emergencyStop = !emergencyStop;

            if(emergencyStop)
            {
                myMusic.Pause();
                Time.timeScale = 0;
            }
            else{Time.timeScale = 1; myMusic.UnPause();}
        }

        if (!startPlaying)
        {
            if(Input.anyKeyDown || Manager.instance.gameData.aY <= -7000)
            {
                startPlaying = true;
                BS.hasStart = true;

                myMusic.Play();
            }
        }
        else
        {
            if(!myMusic.isPlaying && !resultScreen.activeInHierarchy && !emergencyStop)
            {
                resultScreen.SetActive(true);

                normalCounter.text = "" + normalHits;
                perfectCounter.text = "" + perfectHits;
                missedCounter.text = "" + missHits;

                float totalhit = normalHits + perfectHits;

                totalCounter.text = "" + Score; 
            }
        }
    }

    public void NoteHit() 
    {
        //Score += ScorePerNote * multiply;
        multiplyTimes ++;
        multiply = baseNumber + multiplyTimes * 0.1f;

        multiply = (float)Math.Round((decimal)multiply, 1);

        scoreText.text = "Score: " + Score;
        // Debug.Log("Hit");
    }

    public void NoteMiss() 
    {
        multiplyTimes = 0;
        multiply = 1;
        missHits++;
        Debug.Log("MISS");
    }

    public void PerfectHit()
    {
        Score += PerfectScoreValue * multiply;

        NoteHit();
        perfectHits++;
    }

    public void NormalHit()
    {
        Score += BaseScoreValue * multiply;
        NoteHit();
        normalHits++;
    }
}
