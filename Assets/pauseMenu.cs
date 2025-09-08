using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject menu;
    [SerializeField]
    StartGameScene gameStart;
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7)) && gameStart.gameStart)
        {
            Time.timeScale = 0f;
            menu.SetActive(true);
        }
        if (menu.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                returnToGame();
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                exitGame();
            }
        }
    }

    public void exitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu",LoadSceneMode.Single);    
    }

    public void returnToGame()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
    }
}
