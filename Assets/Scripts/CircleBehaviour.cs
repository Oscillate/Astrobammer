using UnityEngine;
using System.Collections;

public class CircleBehaviour : MonoBehaviour {

	private Rigidbody2D rb;
	public Vector2 Speed;
	public int maxSpeed;
	public int brakeStrength;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
		maxSpeed = 150;
		brakeStrength = 5;
	}

	// Update is called once per frame
	void Update () {
		float xDir = Input.GetAxis("Horizontal");
		float yDir = Input.GetAxis("Vertical");

		if (xDir < 0) {
			if (rb.velocity.x > -maxSpeed) {
				rb.AddForce (Vector2.right * xDir *Speed.x);
			}
		}
		else if (xDir > 0) {
			if (rb.velocity.x < maxSpeed) {
				rb.AddForce (Vector2.right * xDir * Speed.x);
			}
		}

		if (yDir < 0) {
			if (rb.velocity.y > -maxSpeed) {
				rb.AddForce (Vector2.up * yDir * Speed.y);
			}
		}
		else if (yDir > 0) {
			if (rb.velocity.y < maxSpeed){
				rb.AddForce (Vector2.up * yDir * Speed.y);
			}
		}

		if (Input.GetKey("x")) {
			rb.AddForce (Vector2.up * -rb.velocity.y*brakeStrength);
			rb.AddForce (Vector2.right * -rb.velocity.x*brakeStrength);
		}

		Vector3 mousePosReal = Input.mousePosition;
		mousePosReal.z = 10;
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosReal);
		rb.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
	}
}
