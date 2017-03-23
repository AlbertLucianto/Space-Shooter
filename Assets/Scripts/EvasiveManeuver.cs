using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour {

	public float dodge;
	public float smoothing;
	public float tilt;

	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public Vector2 startWait;
	public Boundary boundary;

	private float targetManeouver;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		StartCoroutine (Evade ());
	}

	IEnumerator Evade(){
		yield return new WaitForSeconds (Random.Range(startWait.x, startWait.y));

		while (true) {
			targetManeouver = Random.Range (1, dodge)* -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds (Random.Range(maneuverTime.x, maneuverTime.y));
			targetManeouver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
		}
	}
	void FixedUpdate () {
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeouver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuver, 0.0f, rb.velocity.z);
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.minX, boundary.maxX),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.minZ, boundary.maxZ)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}
