using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour {

	public float speed;
	public float tumble;
	public int endurance;

	private Rigidbody rb;

	void Awake() {
		rb = transform.GetComponent<Rigidbody> ();
	}

	void Start(){
		rb.velocity=new Vector3(0,0,-speed);
		Destroy (gameObject, 5);
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
		
	void Update () {
		if (endurance < 0) 
			Destroy (transform.gameObject);
	}
}
