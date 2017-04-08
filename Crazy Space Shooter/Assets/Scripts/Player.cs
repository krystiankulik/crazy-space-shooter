using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public Vector3 startPosition;
	public float minX, maxX;
	public float speed;
	public float tilt;
	public float fireRate;
	public GameObject shot;
	public GameObject shotSpawner;
	public GameObject explosion;
	public GameObject gameController;
	public GameObject shieldBubble;
	public GameObject afterPortal;
	public float burning;
	public float addFuelAmmount;
	public float reverseTime;
	public float shieldTime;
	public float bulletTime;
	public bool shield;
	public bool ifSuperAmmo;
	public float fuel;
	public GameObject reverseText;

	private Text rt;
	private Rigidbody rb;
	private float nextShot;
	private bool skullTaken;
	private Vector3 movement;
	private AudioSource audioSource;
	private bool reversed;
	private bool isFuel;
	public Image fuelImage;


	void Awake(){
		skullTaken = false;
		isFuel = true;
		reversed = false;
		shield = false;
		rb = transform.gameObject.GetComponent<Rigidbody>();
		fuel = 100;
		audioSource = transform.GetComponent<AudioSource>();
		reverseText.SetActive (false);
		rt=reverseText.GetComponent<Text> ();
	}

	void Start(){
		transform.position = startPosition;
	}

	void Update(){

		if (fuel <= 0) {
			isFuel = false;
		}

		if (skullTaken &&! reversed) {
			reversed = true;
			StartCoroutine ("ReverseColor");
		}

		if (isFuel) {
			if (Input.GetButton ("Fire1") && Time.time > nextShot) {
				nextShot = Time.time + fireRate;
				audioSource.Play ();
				if (ifSuperAmmo) {
					Instantiate (shot, shotSpawner.transform.position, Quaternion.identity);
					Instantiate (shot, shotSpawner.transform.position, Quaternion.Euler (new Vector3 (0, 330, 0)));
					Instantiate (shot, shotSpawner.transform.position, Quaternion.Euler (new Vector3 (0, 30, 0)));
				} else
					Instantiate (shot, shotSpawner.transform.position, Quaternion.identity);
			}
			fuel -= Time.deltaTime * burning;
		} else {
			gameController.SendMessage ("GameOver");
		}

		fuelImage.fillAmount = fuel / 100;
	}

	void FixedUpdate () {
		if (isFuel) {
			float move = Input.GetAxis ("Horizontal");
			if (!skullTaken)
				movement = new Vector3 (move, 0, 0);
			else
				movement = new Vector3 (-move, 0, 0);

			rb.velocity = movement * speed;
			Vector3 position = new Vector3 (Mathf.Clamp (transform.position.x, minX, maxX), transform.position.y, transform.position.z);
			rb.position = position;
			rb.rotation = Quaternion.Euler (0f, 0f, rb.velocity.x * -tilt);
		}
	}

	void OnTriggerEnter (Collider col) {

		if ((col.gameObject.tag == "Asteroid" || col.gameObject.tag == "Enemy") && !shield) {

			Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (col.gameObject);
			GameController gC = gameController.GetComponent<GameController> ();
			gC.SendMessage ("GameOver");
			Destroy (transform.gameObject);
		}


		if (col.gameObject.tag == "Fuel") {
			fuel += addFuelAmmount;
			if (fuel > 100)
				fuel = 100;
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "Reverse") {
			if(!shield)
				StartCoroutine ("Reverse");
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "Shield") {
			if (!shield)
				StartCoroutine ("Shield");
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "Bullet") {
			StartCoroutine ("Bullet");
			Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "Portal") {
			Instantiate (afterPortal, transform.position, Quaternion.identity);
			SceneManager.LoadScene ("Bonus");
			Destroy (col.gameObject);
			Destroy (transform.gameObject);
		}

		if (col.gameObject.tag == "Star") {
			Destroy (col.gameObject);
		}
	}

	IEnumerator Reverse () {
		skullTaken = true;
		reverseText.SetActive (true);
		yield return new WaitForSeconds (reverseTime);
		skullTaken = false;
		reverseText.SetActive (false);
	}

	IEnumerator Shield () {
		shield = true;
		GameObject shieldBubbleInst= Instantiate(shieldBubble, new Vector3(transform.position.x, transform.position.y-0.4f, transform.position.z+1.3f), Quaternion.identity) as GameObject;
		yield return new WaitForSeconds (shieldTime);
		Destroy(shieldBubbleInst);
		shield=false;
	}

	IEnumerator Bullet () {
		ifSuperAmmo = true;
		yield return new WaitForSeconds (bulletTime);
		ifSuperAmmo = false;
	}
		
	IEnumerator ReverseColor () {
		yield return new WaitForSeconds(0.5f);
		if (rt.color == Color.white)
			rt.color = Color.red;
		else
			rt.color = Color.white;
		reversed = false;
	}
}
