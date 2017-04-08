using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject shotObject;
	public GameObject shotSpawner;
	public float shotSpeed;
	public float fireDelay;
	public float speed;
	public Vector2 startWait;
	public float dodge;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public float smoothing;
	public float tilt;
	public float minX;
	public float maxX;

	private Rigidbody rb;
	private float targetManeuver;
	private float nextShot;

	void Awake() {
		rb=transform.gameObject.GetComponent<Rigidbody>();
	}

	void Start () {
		Destroy (transform.gameObject, 3);
		rb.velocity = new Vector3 (0f, 0f, -speed);
		rb = transform.gameObject.GetComponent<Rigidbody>();
		InvokeRepeating ("Fire", 1f, fireDelay);
		StartCoroutine (Evade());
	}

	void FixedUpdate(){
		float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		rb.velocity = new Vector3 (newManeuver, 0f, rb.velocity.z);
		rb.rotation= Quaternion.Euler( new Vector3(0,180,rb.velocity.x*tilt));
		rb.position = new Vector3 (Mathf.Clamp (rb.position.x, minX, maxX), rb.position.y, rb.position.z);
	}

	IEnumerator Evade () {
		
		yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

		while (true) {
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign (rb.velocity.x);
			yield return new WaitForSeconds (Random.Range(maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverTime.y));
		}
	}

	void Fire() {
		GameObject shot=Instantiate (shotObject, shotSpawner.transform.position, Quaternion.identity) as GameObject;
		Shot shotComponent = shot.GetComponent<Shot> ();
		shotComponent.speed = -shotSpeed - speed;
	}
}
