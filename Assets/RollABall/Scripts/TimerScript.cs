using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    float gameTimeIndex;
    float gameTime = 150;

    [SerializeField]
    TMP_Text timerText;

    Vector3 originalPos;

    [SerializeField]
    Vector3 runningOutPos;

    [SerializeField]
    Color runningOutColor;

    Color originalColor;

    RectTransform timerTransform;

    public bool gameStart = false;

    GameScoring gameScoring;

    void Start()
    {
        gameTimeIndex = gameTime;
        gameScoring = GameObject.FindGameObjectWithTag("ScoreTracker").GetComponent<GameScoring>();
        timerTransform = timerText.GetComponent<RectTransform>();
        originalPos = timerTransform.localPosition;
        originalColor = timerText.color;
    }

    // Update is called once per frame
    void Update()
    {
        
        double minute = Mathf.FloorToInt(gameTimeIndex / 60);
        double seconds = Math.Truncate((gameTimeIndex - (60 * minute)));
        double miliseconds = ((Math.Truncate((gameTimeIndex - (60 * minute)) * 100) / 100) % 1) * 100;

        

        if (gameTimeIndex > 10)
        {
            timerText.text = string.Format("{0:0}:{1:00}:{2:00}", minute, seconds, miliseconds);
            timerTransform.localPosition = originalPos;
            timerText.color = originalColor;
        }
        else
        {
            timerText.text = string.Format("{0:00}:{1:00}",seconds,miliseconds);
            timerTransform.localPosition = runningOutPos;
            timerText.color = runningOutColor;
        }

        if (Time.timeScale != 1)
        {
            timerText.text = "2:30:00";
            timerTransform.localPosition = originalPos;
            timerText.color = originalColor;
        }

        if (gameTimeIndex > 0)
        {
            gameTimeIndex -= Time.deltaTime;
        }
        else
        {
            gameScoring.switchToScoreScreen();
            gameTimeIndex = 0;
        }
    }
}
