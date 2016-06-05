﻿using UnityEngine;
using System.Collections;

public class CircleBehaviour : MonoBehaviour {

    private Rigidbody2D rb;
    private Vector3 prevMouse;
    public float speed = 150;
    public float maxSpeed = 150;
    public float brakeStrength = 5;
    public int playerNum = 1;
    public int batCoolDown = 15;
    public int drillCoolDown = 15;
    private int batTimer = 0;
    private int drillTimer = 0;
    public GameObject bat;
    public GameObject drill;
    public GameObject explosive;
    private Object myDrill;
    private Object myBat;
    private Quaternion rotation;
    public int bombInventory = 30;
    private bool isWrap = false;

    void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        GameManager.players.Add(this.gameObject);
    }

    private float GetAxis(string axisName) {
        return Input.GetAxis("P" + playerNum + " " + axisName);
    }

    private bool GetButton(string buttonName) {
        return Input.GetButton("P" + playerNum + " " + buttonName);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.collider.sharedMaterial.name.Equals("Asteroid")) {
            if (coll.collider.GetComponent<Asteroid>().isLethal) {
              Die();
            }
        }
    }

    void Die() {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
        float xDir = GetAxis("Move Horizontal");
        float yDir = GetAxis("Move Vertical");

        if (xDir < 0) {
            if (rb.velocity.x > -maxSpeed) {
                rb.AddForce(Vector2.right * xDir * speed);
            }
        } else if (xDir > 0) {
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
            if (rb.velocity.y < maxSpeed) {
                rb.AddForce(Vector2.up * yDir * speed);
            }
        }

        if (batTimer > 0) {
            batTimer -= 1;
        }
        if (drillTimer > 0) {
            drillTimer -= 1;
        }
        if (GetAxis("Fire1") > 0 && batTimer == 0) {
            myBat = Instantiate(bat, this.transform.position + this.transform.up * 5, rotation);
            ((GameObject)myBat).transform.SetParent(this.transform);
            batTimer = batCoolDown;
        }

        if (GetButton("Fire2") && drillTimer == 0) {
            myDrill = Instantiate(drill, this.transform.position + this.transform.up * 5, rotation);
            ((GameObject)myDrill).transform.SetParent(this.transform);
            drillTimer = drillCoolDown;
        }

        if (GetAxis("Fire3") > 0) {
            rb.AddForce (Vector2.up * -rb.velocity.y * brakeStrength);
            rb.AddForce (Vector2.right * -rb.velocity.x * brakeStrength);
        }

        // TODO get a fire button
        if (GetButton("Fire4") && bombInventory > 0) {
            Instantiate(explosive, this.transform.position + this.transform.up * 10, rb.transform.rotation);
            bombInventory--;
        }

        float joyX = GetAxis("Look Horizontal");
        float joyY = GetAxis("Look Vertical");
        if (joyX != 0 || joyY != 0) {
            float joyAngle = Mathf.Atan2(joyY, joyX) * -1 * 180 / Mathf.PI - 90;
            rotation = Quaternion.AngleAxis(joyAngle, Vector3.forward);
        }
        if (playerNum == 1 && prevMouse != Input.mousePosition) {
            Vector3 mousePosReal = Input.mousePosition;
            mousePosReal.z = 100;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosReal);
            rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        }
        rb.transform.rotation = rotation;

        prevMouse = Input.mousePosition;

        if (!isWrap && !GetComponent<SpriteRenderer> ().isVisible) {
            Vector3 renderpos = Camera.main.WorldToViewportPoint (transform.position);
            if (renderpos.x > 1 || renderpos.x < 0) {
                transform.position = new Vector3(-transform.position.x,transform.position.y,transform.position.z);
            }
            if (renderpos.y > 1 || renderpos.y < 0) {
                transform.position = new Vector3(transform.position.x,-transform.position.y,transform.position.z);
            }
            isWrap = true;
            //transform.position = Camera.main.ViewportToWorldPoint (renderpos);
        } else if (GetComponent<SpriteRenderer> ().isVisible) {
            isWrap = false;
        }
    }

    void OnDestroy(){
        GameManager.players.Remove (this.gameObject);
    }
}
