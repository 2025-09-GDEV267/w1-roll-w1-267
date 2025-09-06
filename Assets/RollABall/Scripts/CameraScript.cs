using System.Linq;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    float Sensitivity;
    float LookXController;
    float LookXKeys;

    void Start()
    {
       
    }
    private void Update()
    {
        LookXController = Input.GetAxisRaw("LookXController");
        LookXKeys = Input.GetAxisRaw("LookXKeyboard");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = playerTransform.position;

        if (Mathf.Abs(LookXController) > Mathf.Abs(LookXKeys))
        {
            transform.Rotate(new Vector3(0, LookXController * Sensitivity, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, LookXKeys * Sensitivity, 0));
        }

        Debug.Log(LookXController);
        Debug.Log(LookXKeys);
    }
}
