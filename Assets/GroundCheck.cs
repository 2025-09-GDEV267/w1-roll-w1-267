using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGrounded;
    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        Debug.Log("Grounded");
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
        Debug.Log("Airborne");
    }

    public bool getGrounded()
    {
        return isGrounded;
    }
}
