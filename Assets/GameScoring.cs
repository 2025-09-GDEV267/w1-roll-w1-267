using System;
using TMPro;
using UnityEngine;

public class GameScoring : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float gameTime = 150;
    float gameTimeIndex;
    [SerializeField]
    TMP_Text timerText;
    void Start()
    {
        gameTimeIndex = gameTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        double minute = Mathf.FloorToInt(gameTimeIndex/60);
        double seconds = Math.Truncate((gameTimeIndex - (60 * minute)));
        double miliseconds = ((Math.Truncate((gameTimeIndex - (60 * minute)) * 100)/100) % 1) * 100;

        timerText.text = string.Format("{0:0}:{1:00}:{2:00}",minute,seconds,miliseconds);

        if (gameTimeIndex > 0)
        {
            gameTimeIndex -= Time.deltaTime;
        }
        else
        {
            switchToScoreScreen();
            gameTimeIndex = 0;
        }
    }

    void switchToScoreScreen()
    {
        
    }
}
