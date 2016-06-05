using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public Canvas UI;
	public Text status;
	public Button playButton;
	public static List<GameObject> players;

	private bool gameStarted;
	// Use this for initialization

	void Awake(){
		players = new List<GameObject>();
	}

	void Start () {
		gameStarted = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameStarted && players.Count == 1) {
			GameOver (players[0]);
		} 
		if (Input.GetButtonDown("Restart")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}

	public void Play(){
		UI.enabled = false;
		gameStarted = true;
	}

	void GameOver(GameObject winner){
		UI.gameObject.SetActive(true);
		gameStarted = false;
		Debug.Log ("Game Over");
		playButton.onClick.RemoveAllListeners ();
		playButton.onClick.AddListener (() => {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		});
		status.text = winner.name + " wins!";
	}
}
