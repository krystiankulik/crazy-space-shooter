using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBehaviour: MonoBehaviour {

	public float speed;
	public Vector3 rotation;
	public Vector3 localScale;

	private Rigidbody rb;

	void Start() {
		transform.localScale = localScale;
		transform.Rotate(rotation);
		rb = transform.GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (0f, 0f, -speed);
		Destroy (transform.gameObject, 3f);
	}
}
