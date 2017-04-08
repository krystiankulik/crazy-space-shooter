using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BonusController : MonoBehaviour {

	public float minX, maxX;
	public float posY, posZ;
	public float spawnStarTime;
	public GameObject starObject;
	public Text text;
	public int score;
	public int highScore;
	public float time;
	public float bonusTime;

	private Text t;
	private string highScoreString="HighScore";

	void Awake () {
		time = 0;
	}
		
	void Start () {
		score = PlayerPrefs.GetInt ("Score", 0);
		highScore = PlayerPrefs.GetInt ("HighScore", 0);
		InvokeRepeating ("SpawnStar", 0, spawnStarTime);
	}
		
	void Update () {
		time += Time.deltaTime;
		text.text = "Score: " + score;
		if (score > highScore)
			PlayerPrefs.SetInt (highScoreString, score);
		PlayerPrefs.Save ();
		PlayerPrefs.SetInt ("Score", score);
		PlayerPrefs.Save ();

		if (time > bonusTime)
			SceneManager.LoadScene ("MainScene");
	}

	void SpawnStar () {
		Vector3 position = new Vector3 (Random.Range (minX, maxX), posY, posZ);
		Instantiate (starObject, position, Quaternion.identity);
	}


}
