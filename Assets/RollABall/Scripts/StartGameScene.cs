using UnityEngine;

public class StartGameScene : MonoBehaviour
{
    public bool gameStart = false;
    private bool gamePaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStart = false;
        gamePaused = false;
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            if (gamePaused == false)
            {
                gamePaused = true;
                Time.timeScale = 1f;
            }
        }
    }
}
