using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

	public float speed;
	public GameObject explosion;
	private GameObject gameController;

	void Start () {
		Destroy (transform.gameObject, 1.5f);
	}

	void Update () {
		transform.Translate(new Vector3(0,0,speed*Time.deltaTime));
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Asteroid") {
			Asteroids a=col.gameObject.GetComponent<Asteroids> ();
			if (a.endurance == 1) {
				gameController = GameObject.Find ("GameController");
				gameController.SendMessage ("AddScore", 1);
				Instantiate (explosion, transform.position, Quaternion.identity);
			}
			a.endurance=a.endurance-1;
			Destroy (transform.gameObject);
		}

		if (col.gameObject.tag == "Player") {
			Player player = col.gameObject.GetComponent<Player> ();
			if (player.shield==false) {
				Instantiate (explosion, transform.position, Quaternion.identity);
				Destroy (col.gameObject);
				gameController = GameObject.Find ("GameController");
				GameController gC = gameController.GetComponent<GameController> ();
				gC.SendMessage ("GameOver");
			}
			Destroy (transform.gameObject);
		}

		if (col.gameObject.tag == "Enemy") {
			Instantiate (explosion, transform.position, Quaternion.identity);
			gameController = GameObject.Find ("GameController");
			gameController.SendMessage ("AddScore", 3);
			Destroy (col.gameObject);
			Destroy (transform.gameObject);
		}
	}
}
