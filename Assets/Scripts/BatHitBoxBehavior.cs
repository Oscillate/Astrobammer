using UnityEngine;
using System.Collections;

public class BatHitBoxBehavior : MonoBehaviour {
	public int activeFrames;
	// Use this for initialization
	void Start () {
		activeFrames = 5;
	}

	void OnTriggerEnter2D(Collider2D c){
		if 	(c.sharedMaterial.name.Equals("Asteroid")){
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		activeFrames -= 1;
		if (activeFrames < 0) {
			Destroy(gameObject);
		}
	}
}
