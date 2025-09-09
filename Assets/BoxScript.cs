using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    Vector3 originalPos;
    Quaternion originalRotation;
    bool reset = true;
    Rigidbody rb;
    // Update is called once per frame
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Invoke("setOriginalPos",1f);
    }
    void Update()
    {
        if (originalPos != null)
        {
            if ((transform.position != originalPos || transform.rotation != originalRotation) && reset == true)
            {
                reset = false;
                Invoke("resetPosition", 10f);
            }
        }
    }
    void resetPosition()
    {
        transform.position = originalPos;
        transform.rotation = originalRotation;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        reset = true;
    }
    void setOriginalPos()
    {
        originalPos = transform.position;
        originalRotation = transform.rotation;
    }
}
