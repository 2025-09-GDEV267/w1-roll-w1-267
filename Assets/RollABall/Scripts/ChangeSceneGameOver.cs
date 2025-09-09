using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneGameOver : MonoBehaviour
{
    public bool ChangeScene;

    private void Awake()
    {
        ChangeScene = false;
    }

    private void Update()
    {
        if (ChangeScene)
        {
            SceneManager.LoadScene("ScoreScreen",LoadSceneMode.Single);
        }
    }
}
