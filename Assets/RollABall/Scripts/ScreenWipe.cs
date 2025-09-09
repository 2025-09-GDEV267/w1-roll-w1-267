using UnityEngine;

public class ScreenWipe : MonoBehaviour
{
    [SerializeField]
    Animator ScreenWipeAnim;
    public void StartScreenWipe()
    {
        ScreenWipeAnim.Play("ScreenWipe");
    }

    public void StartScreenWipeAway()
    {
        ScreenWipeAnim.Play("ScreenWipeAway");
    }
}
