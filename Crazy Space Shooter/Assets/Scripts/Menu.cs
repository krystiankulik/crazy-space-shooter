using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject loadingText;

	void Start () {
		loadingText.SetActive (false);
	}
		
	public void OnPlayClick () {
		loadingText.SetActive (true);
		SceneManager.LoadScene ("MainScene");
	}

	public void OnQuitClick () {
		Application.Quit ();
	}
}

