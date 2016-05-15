using UnityEngine;
using System.Collections;

public class CircleBehaviour : MonoBehaviour {

	private Rigidbody2D rb;
	public int vertSpeed;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("w")) {
			rb.AddForce (transform.up * vertSpeed);
		}
		if (Input.GetKey ("s")) {
			rb.AddForce (transform.up * -vertSpeed);
		}
	}
}
