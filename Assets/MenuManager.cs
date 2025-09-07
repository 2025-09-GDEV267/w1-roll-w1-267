using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static void startGame()
    {
        SceneManager.LoadScene("Roll-a-ball", LoadSceneMode.Single);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            startGame();
        }
    }
}
