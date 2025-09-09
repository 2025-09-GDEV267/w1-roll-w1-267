using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScoring : MonoBehaviour
{
    public static GameScoring instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    ComboTracker tracker;

    [SerializeField]
    float score = 0;

    public bool timerStarted;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void StartGame()
    {
        tracker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ComboTracker>();
    }

    public float getScore()
    {
        return score;
    }
    public void switchToScoreScreen()
    {
        GameObject.FindGameObjectWithTag("Finish").GetComponent<Animator>().Play("GameOverAnimation");
        DontDestroyOnLoad(gameObject);
        Time.timeScale = 0;
        score = tracker.Score;
    }
}
