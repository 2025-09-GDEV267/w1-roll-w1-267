using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    [SerializeField]
    GameObject yellowArrowThing;

    public void Active()
    {
        yellowArrowThing.SetActive(true);
    }
    public void NotActive()
    {
        yellowArrowThing.SetActive(false);
    }
}
