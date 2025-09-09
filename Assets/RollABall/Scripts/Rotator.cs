using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	[SerializeField]
	GameObject rotatee;
	[SerializeField]
	ParticleSystem particles;
	[SerializeField]
	float respawnTime;
	[SerializeField]
	AudioSource audioSource;

	private float timeIndex;

	[SerializeField]
	GameObject gameManager;
    // Before rendering each frame..
    private void Awake()
    {
		gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    void Update()
	{
		if (timeIndex > 0)
		{
			timeIndex -= Time.deltaTime;
		} else if (timeIndex <= 0)
		{
			respawn();
		}

		rotatee.transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
	public void dissapear()
	{
		rotatee.SetActive(false);
		particles.Play();
		timeIndex = respawnTime;
		audioSource.pitch = Random.Range(1f,1.3f);
		audioSource.Play();
		gameObject.GetComponent<BoxCollider>().enabled = false;
	}

	public void respawn()
	{
		gameObject.GetComponent<BoxCollider>().enabled = true;
		rotatee.SetActive(true);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
			dissapear();
			gameManager.GetComponent<ComboTracker>().Collect();
        }
    }
}	