using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool changeScenes;
    public Animator animator;
    private void Start()
    {
        changeScenes = false;
    }
    public void startGameButton()
    {
        animator.Play("MenuAnim");
    }

    public void quitGame()
    {
        Debug.Log("Game Close");
        Application.Quit();
    }
    public void startGame()
    {
        SceneManager.LoadScene("Roll-a-ball", LoadSceneMode.Single);
    }

    private void Update()
    {
        if (changeScenes)
        {
            startGame();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            startGameButton();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            quitGame();
        }
    }
}
