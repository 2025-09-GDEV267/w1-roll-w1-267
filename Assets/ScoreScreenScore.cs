using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScreenScore : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameScoring gameScoring;
    [SerializeField]
    TMP_Text scoreText;
    void Start()
    {
        Time.timeScale = 1f;
        gameScoring = GameObject.FindGameObjectWithTag("ScoreTracker").GetComponent<GameScoring>();
        
    }

    private void Update()
    {
        if (gameScoring != null && scoreText != null)
        {
            if (gameScoring.getScore() < 999999)
            {
                scoreText.text = gameScoring.getScore().ToString();
            }
            else if (gameScoring.getScore() < 999999999)
            {
                scoreText.text = Math.Round(gameScoring.getScore() / 1000000f, 2) + "M";
            }
            else
            {
                scoreText.text = "Too much!\n" + gameScoring.getScore().ToString(); 
            }
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            playAgain();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            goBackToMenu();
        }
    }

    public void goBackToMenu()
    {
        SceneManager.LoadScene("Menu",LoadSceneMode.Single);
    }
    public void playAgain()
    {
        SceneManager.LoadScene("Roll-a-ball", LoadSceneMode.Single);
    }
}
