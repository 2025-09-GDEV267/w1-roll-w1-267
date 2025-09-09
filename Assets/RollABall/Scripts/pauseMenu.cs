using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    GameObject menu;
    [SerializeField]
    StartGameScene gameStart;
    [SerializeField]
    GameObject Combo;
    [SerializeField]
    TMP_Text timerText;
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
            Combo.SetActive(false);
            timerText.enabled = false;
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
        Combo.SetActive(true);
        timerText.enabled = true;
        Time.timeScale = 1f;
        menu.SetActive(false);
    }
}
