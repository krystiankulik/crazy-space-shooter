using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	private GameObject player;

	void Start () {
		player = GameObject.Find ("PlayerShip");
	}
		
	void Update () {
		if(player != null)
		transform.position = new Vector3 (player.transform.position.x, player.transform.position.y , player.transform.position.z);
	}
}
