using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AsteroidManager : MonoBehaviour {

	public GameObject asteroidPrefab;

	public static List<Asteroid> Asteroids;

	public float initialSpeed;
	public float timeBetweenSpawns;

	private float lastSpawn;
	 
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
		GameObject newAsteroid = Instantiate (asteroidPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		float newAngle = Random.value * 2 * Mathf.PI;
		newAsteroid.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Mathf.Cos (newAngle) * initialSpeed, Mathf.Sin (newAngle) * initialSpeed);
	}
}
