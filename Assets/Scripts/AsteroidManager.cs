using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AsteroidManager : MonoBehaviour {

	public GameObject asteroidPrefab;

	public static List<Asteroid> Asteroids;

	public float initialSpeed;
	public float timeBetweenSpawns;

	private float lastSpawn;

	public float generativeOffset;

	void Awake(){
		Asteroids = new List<Asteroid> ();
	}

	// Use this for initialization
	void Start () {
		lastSpawn = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - lastSpawn > timeBetweenSpawns) {
			SpawnAsteroid ();
			lastSpawn = Time.time;
		}
	}

	void SpawnAsteroid(){
		
		Vector3 screenLocation, worldLocation;
		screenLocation = Vector3.zero;
		screenLocation.x = Random.value;
		screenLocation.y = Random.value;
		int edge = Random.Range (0, 4);
		switch (edge) {
		case 0:
			screenLocation.x = 0 - generativeOffset;
		break;
			case 1:
			screenLocation.y = 0 - generativeOffset;
		break;
			case 2:
			screenLocation.x = 1 + generativeOffset;
		break;
			case 3:
			screenLocation.y = 1 + generativeOffset;
		break;
		}
		worldLocation = Camera.main.ViewportToWorldPoint(screenLocation);
		worldLocation.z = 0;
		GameObject newAsteroid = Instantiate (asteroidPrefab, worldLocation, Quaternion.identity) as GameObject;
		screenLocation = Vector3.zero;
		screenLocation.x = Random.value;
		screenLocation.y = Random.value;
		worldLocation = Camera.main.ViewportToWorldPoint(screenLocation);
		newAsteroid.GetComponent<Rigidbody2D> ().velocity = Vector3.Normalize(worldLocation - newAsteroid.transform.position) * initialSpeed;
	}
}
