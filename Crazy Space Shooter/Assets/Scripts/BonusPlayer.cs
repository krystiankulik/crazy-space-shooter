using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusPlayer : MonoBehaviour {

	public Vector3 startPosition;
	public float speed;
	public float tilt;
	public float minX, maxX;
	public BonusController bc;

	private Rigidbody rb;
	private Vector3 movement;
	private AudioSource audioSource;

	void Awake () {
		audioSource = transform.GetComponent<AudioSource> ();
	}

	void Start () {
		transform.position = startPosition;
		rb = transform.gameObject.GetComponent<Rigidbody>();
	}

	void FixedUpdate () {

		float move=Input.GetAxis("Horizontal");
		movement = new Vector3 (move, 0, 0);
		rb.velocity = movement * speed;
		Vector3 position = new Vector3 (Mathf.Clamp (transform.position.x, minX, maxX), transform.position.y, transform.position.z);
		rb.position = position;
		rb.rotation = Quaternion.Euler (0f, 0f, rb.velocity.x * -tilt);
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Star") {
			audioSource.Play ();
			bc.score++;
			Destroy (col.gameObject);
		}
	}








}
