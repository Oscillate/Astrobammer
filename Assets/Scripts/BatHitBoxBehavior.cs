using UnityEngine;
using System.Collections;

public class BatHitBoxBehavior : MonoBehaviour {
	public int activeFrames;
	public int batStrength;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		activeFrames -= 1;
		if (activeFrames < 0) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		coll.rigidbody.AddForce(this.transform.up * batStrength);
	}
}
