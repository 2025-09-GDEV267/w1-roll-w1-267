using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGrounded;

    float groundDistance = 0.2f;
    public LayerMask groundMask;

    private void Start()
    {
        isGrounded = false;
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance,groundMask);
    }

    public bool getGrounded()
    {
        return isGrounded;
    }
}
