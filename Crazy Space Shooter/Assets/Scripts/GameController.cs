using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public float minX, maxX;
	public float posY, posZ;
	public GameObject[] asteroids= new GameObject[3];
	public GameObject enemy;
	public float spawnAsteroidTime;
	public float spawnEnemyTime;
	public float spawnFuelTime;
	public float spawnReverseTime;
	public float spawnShieldTime;
	public float spawnBulletTime;
	public float spawnPortalTime;
	public GameObject fuelObject;
	public GameObject reverseObject;
	public GameObject shieldObject;
	public GameObject bulletObject;
	public GameObject portalObject;
	public GameObject bestScoreText;
	public Text text;
	public GameObject gameOverText;
	public GameObject playAgain;
	public GameObject quit;
	public int score;
	public int highScore;
	public GameObject reverseText;

	private bool gameIsOver;
	private Text t;
	private string highScoreString = "HighScore";
	private string scoreString = "Score";

	void Awake () {

		gameIsOver = false;
		gameOverText.SetActive(false);
		playAgain.SetActive(false);
		quit.SetActive(false);
		bestScoreText.SetActive (false);
		t = bestScoreText.GetComponent<Text> ();
	}
		
	void Start () {

		highScore = PlayerPrefs.GetInt (highScoreString, 0);
		score = PlayerPrefs.GetInt (scoreString, 0);
		InvokeRepeating ("SpawnFuel", 0.7f, spawnFuelTime);
		InvokeRepeating ("SpawnAsteroid", 0f, spawnAsteroidTime);
		InvokeRepeating ("SpawnEnemies", 1.5f, spawnEnemyTime);
		InvokeRepeating ("SpawnReverse", 2.7f, spawnReverseTime);
		InvokeRepeating("SpawnShield", 3.2f, spawnShieldTime);
		InvokeRepeating ("SpawnBullet", 4.7f, spawnBulletTime);
		InvokeRepeating ("SpawnPortal", 5.7f, spawnPortalTime);

	}
		
	void Update () {
		t.text = "Best Score: " + highScore;
		text.text = "Score: " + score;

		if (score > highScore) {
			PlayerPrefs.SetInt (highScoreString, score);
		}

		if (!gameIsOver) {
			PlayerPrefs.SetInt (scoreString, score);
		}

		PlayerPrefs.Save ();
	}

	void SpawnEnemies () {

		Vector3 position = new Vector3 (Random.Range (minX, maxX), posY, posZ);
		Instantiate (enemy, position, Quaternion.identity);
	}

	void SpawnAsteroid () {

		Vector3 position = new Vector3 (Random.Range (minX, maxX), posY, posZ);
		int index = Random.Range (0, 2);
		Instantiate (asteroids [index], position, Quaternion.identity);
	}
		
	void GameOver () {

		gameIsOver = true;
		gameOverText.SetActive(true);
		playAgain.SetActive(true);
		quit.SetActive(true);
		bestScoreText.SetActive (true);
		reverseText.SetActive (false);
		PlayerPrefs.SetInt (scoreString, 0);
		PlayerPrefs.Save ();

	}

	void AddScore (int value) {
		score += value;
	}

	void SpawnFuel () {
		Vector3 position = new Vector3 (Random.Range (minX, maxX), posY, posZ);
		Instantiate (fuelObject, position, Quaternion.identity);
	}

	void SpawnReverse () {
		Vector3 position = new Vector3 (Random.Range (minX, maxX), posY, posZ);
		Instantiate (reverseObject, position, Quaternion.identity);
	}

	void SpawnShield () {
		Vector3 position = new Vector3 (Random.Range (minX, maxX), posY, posZ);
		Instantiate (shieldObject, position, Quaternion.identity);
	}

	void SpawnBullet () {
		Vector3 position = new Vector3 (Random.Range (minX, maxX), posY, posZ);
		Instantiate (bulletObject, position, Quaternion.identity);
	}

	void SpawnPortal () {
		Vector3 position = new Vector3 (Random.Range (minX, maxX), posY, posZ);
		Instantiate (portalObject, position, Quaternion.identity);
	}

	public void OnPlayClick () {
		SceneManager.LoadScene ("MainScene");
	}

	public void OnQuitClick () {
		Application.Quit ();
	}
}
