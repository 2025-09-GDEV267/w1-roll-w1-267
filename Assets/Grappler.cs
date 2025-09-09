using System.Data.Common;
using UnityEngine;
using UnityEngine.Device;

public class Grappler : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private Vector3 grapplePoint;
    public Transform grappleTip, Player;
    public Camera cam;
    public GameObject[] grappleObjects;
    public GameObject Closest;
    public float Range;
    private SpringJoint joint;

    private bool isGrappled;

    public ComboTracker combo;

    private void Start()
    {
        grappleObjects = GameObject.FindGameObjectsWithTag("grapple");
    }

    private void Update()
    {

        foreach (GameObject i in grappleObjects)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(i.transform.position);
            bool onScreen = screenPos.x > 0f && screenPos.x < UnityEngine.Screen.width && screenPos.y > 0f && screenPos.y < UnityEngine.Screen.height;

            float itteration = Vector3.Dot(cam.transform.forward, (i.transform.position - cam.transform.position).normalized);

            Debug.Log(itteration);

            if (Closest != null)
            {

                if (Closest != i)
                {
                    if (itteration > Vector3.Dot(cam.transform.forward, (Closest.transform.position - cam.transform.position).normalized) && itteration > .90f && Vector3.Distance(i.transform.position, gameObject.transform.position) < Range)
                    {
                        Closest.GetComponent<GrapplePoint>().NotActive();
                        Closest = i;
                        Closest.GetComponent<GrapplePoint>().Active();
                    }
                    else if (Vector3.Dot(cam.transform.forward, (Closest.transform.position - cam.transform.position).normalized) < .90f || Vector3.Distance(Closest.transform.position, gameObject.transform.position) > Range)
                    {
                        Closest.GetComponent<GrapplePoint>().NotActive();
                        Closest = null;
                    }
                }
            } else if (itteration > .90f && Vector3.Distance(i.transform.position, gameObject.transform.position) < Range)
            {
                Closest = i;
                Closest.GetComponent<GrapplePoint>().Active();
            }

        }



        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            StartGrapple();
            
        }
        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            StopGrapple();
            combo.setIsGrappled(false, grapplePoint);
        }

    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void StartGrapple()
    {
        isGrappled = true;
        if (Closest != null)
        {
            combo.setIsGrappled(true, grapplePoint);
            DrawRope();
            grapplePoint = Closest.transform.position;
            joint = Player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(Player.position, grapplePoint);



            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lineRenderer.positionCount = 2;
        }
    }

    void DrawRope()
    {
        if (!joint) return;
        if (isGrappled)
        {
            lineRenderer.SetPosition(0, grappleTip.transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }    
    }

    public void StopGrapple()
    {
        isGrappled = false;
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }

    public bool getGrappled()
    {
        return isGrappled;
    }
}
