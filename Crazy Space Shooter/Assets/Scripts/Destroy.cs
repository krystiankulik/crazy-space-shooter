using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	public float lifeTime;

	void Start () {
		Destroy (transform.gameObject, lifeTime);
	}
	

}
