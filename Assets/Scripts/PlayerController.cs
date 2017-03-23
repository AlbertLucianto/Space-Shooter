using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
	public float minX, maxX, minZ, maxZ;
}

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private float nextFire = 0.0f;
	private AudioSource sound;

	public float fireRate;
	public GameObject shot;
	public Transform shotSpawn;
	public float speed;
	public float tiltX, tiltZ;
	public Boundary boundary;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		sound = GetComponent<AudioSource> ();
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.minX , boundary.maxX),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.minZ, boundary.maxZ)
		);
		rb.transform.rotation = Quaternion.Euler (rb.velocity.z*tiltX , 0.0f, rb.velocity.x * tiltZ);
	}

	void Update(){
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			sound.Play ();
		}
	}
}
