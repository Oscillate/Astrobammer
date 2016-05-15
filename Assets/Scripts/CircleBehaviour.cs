using UnityEngine;
using System.Collections;

public class CircleBehaviour : MonoBehaviour {

	private Rigidbody2D rb;
	public Vector2 Speed;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("w")) {
			rb.AddForce (Vector2.up * Speed.x);
		}
		if (Input.GetKey ("s")) {
			rb.AddForce (Vector2.up * -Speed.x);
		}
		if (Input.GetKey ("a")) {
			rb.AddForce (Vector2.right * -Speed.y);
		}
		if (Input.GetKey ("d")) {
			rb.AddForce (Vector2.right * Speed.y);
		}

		Vector3 mousePosReal = Input.mousePosition;
		mousePosReal.z = 10;
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosReal);
		rb.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
	}
}
