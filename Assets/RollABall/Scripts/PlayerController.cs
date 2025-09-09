using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;
using Unity.VisualScripting;
using System;

public class PlayerController : MonoBehaviour {

	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
    float ballSpeed;
	public float jumpForce;
	public float fallMax;
	public Transform spawnPoint;
	public Transform cam;
	public Text countText;
	public Text winText;
	public float maxVelocity;
	// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
	private Rigidbody rb;
	private int count;
	public GroundCheck groundCheck;
	public Grappler grappler;
	public ComboTracker comboTracker;
	[SerializeField]
	ScreenWipe screenWipe;
	[SerializeField]
	AudioSource dead;

	private bool canReset = true;
	// At the start of the game..
	void Start()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;

		ballSpeed = speed;
	}
	private void Update()
	{
		if ( ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Joystick1Button0)) && groundCheck.getGrounded() == true) && Time.timeScale == 1f)
		{
			rb.AddForce(new Vector3(0,jumpForce,0));
		}
		
		if (grappler.getGrappled() == true)
		{
			ballSpeed = speed * 2;
		}
		else
		{
			ballSpeed = speed;
		}
		comboTracker.setIsGrounded(groundCheck.getGrounded());
    }
    // Each physics step..
    void FixedUpdate ()
	{
    
    // Set some local float variables equal to the value of our Horizontal and Vertical Inputs
    float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical).normalized;

		// Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
		// multiplying it by 'speed' - our public player speed that appears in the inspector
		if (movement.magnitude >= 0.1f)
		{
			//Finds the angle the player wants the ball to move in
			float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
			
			Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
			rb.AddForce(moveDir.normalized * ballSpeed);
		}

		if (transform.position.y < fallMax)
		{
			screenWipe.StartScreenWipe();

			if (dead.isPlaying == false)
			{
                dead.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
                dead.Play();
			}
			canReset = false;
			Invoke("Respawn",1.5f);
		}
		if (Time.timeScale == 1 && canReset)
		{
			if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button6))
			{
				canReset = false;
				screenWipe.StartScreenWipe();
				Invoke("Respawn", 1.5f);
			}
		}
	}

	public void Respawn()
	{
		if(grappler.getGrappled()) grappler.StopGrapple();
		comboTracker.getRespawned(true);
        rb.linearVelocity = new Vector3(0, 0, 0);
        transform.position = spawnPoint.position;
		screenWipe.StartScreenWipeAway();
		Invoke("setResetToTrue", 5f);
	}

	private void setResetToTrue()
	{
        canReset = true;

    }
    public int getMagnitude()
	{
		return Convert.ToInt32(rb.linearVelocity.magnitude * 10);
	}
	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
}