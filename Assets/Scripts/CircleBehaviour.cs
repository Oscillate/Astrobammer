using UnityEngine;
using System.Collections;

public class CircleBehaviour : MonoBehaviour {

	private Rigidbody2D rb;
    private Vector3 prevMouse;
	public float speed = 150;
	public float maxSpeed = 150;
	public float brakeStrength = 5;
    public int playerNum = 1;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
    }

    private float GetAxis(string axisName) {
        return Input.GetAxis("P" + playerNum + " " + axisName);
    }

    private bool GetButton(string buttonName) {
        return Input.GetButton("P" + playerNum + " " + buttonName);
    }

    // Update is called once per frame
    void Update () {
		float xDir = GetAxis("Move Horizontal");
		float yDir = GetAxis("Move Vertical");

		if (xDir < 0) {
			if (rb.velocity.x > -maxSpeed) {
				rb.AddForce(Vector2.right * xDir * speed);
			}
		}
		else if (xDir > 0) {
			if (rb.velocity.x < maxSpeed) {
				rb.AddForce(Vector2.right * xDir * speed);
			}
		}

		if (yDir < 0) {
			if (rb.velocity.y > -maxSpeed) {
				rb.AddForce(Vector2.up * yDir * speed);
			}
		}
		else if (yDir > 0) {
			if (rb.velocity.y < maxSpeed){
				rb.AddForce(Vector2.up * yDir * speed);
			}
		}

		if (this.GetButton("Fire2")) {
			rb.AddForce (Vector2.up * -rb.velocity.y * brakeStrength);
			rb.AddForce (Vector2.right * -rb.velocity.x * brakeStrength);
		}
        
        float joyX = GetAxis("Look Horizontal");
        float joyY = GetAxis("Look Vertical");
        if (joyX != 0 || joyY != 0) {
            float joyAngle = Mathf.Atan2(joyY, joyX) * -1 * 180 / Mathf.PI - 90;
            rb.transform.rotation = Quaternion.AngleAxis(joyAngle, Vector3.forward);
        }
        if (playerNum == 1 && prevMouse != Input.mousePosition) {
            Vector3 mousePosReal = Input.mousePosition;
            mousePosReal.z = 100;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosReal);
            rb.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        }

        prevMouse = Input.mousePosition;
	}
}
