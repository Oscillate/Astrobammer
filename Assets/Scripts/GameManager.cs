using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using InControl;

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
    void Update() {
        if (gameStarted && players.Count == 1) {
            GameOver(players[0]);
        }

        bool restart = false;
        for (int i = 0; i < InputManager.Devices.Count; i++) {
            if (InputManager.Devices[i].MenuWasPressed) {
                restart = true;
                break;
            }
        }

        if (restart) {
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
        playButton.onClick.RemoveAllListeners ();
        playButton.onClick.AddListener (() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        status.text = winner.name + " wins!";
    }
}
