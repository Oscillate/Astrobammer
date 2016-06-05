﻿using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

    public enum Size {small, medium, large};
    public Size size;
    public int numChildsSpawnedOnBreak;
    private Rigidbody2D rb;
    public bool isLethal = false;
    private int onSpawnInvincibilityFrames;
    private bool isWrap = false;
    // Use this for initialization
    void Start () {
        AsteroidManager.Asteroids.Add (this);
        rb = this.GetComponent<Rigidbody2D>();
        onSpawnInvincibilityFrames = 6;
    }

    void OnDestroy(){
        AsteroidManager.Asteroids.Remove (this);
    }

    // Update is called once per frame
    void Update () {
        isLethal = GetComponent<Rigidbody2D>().velocity.magnitude > 150;

        if (isLethal) {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        } else {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        if (onSpawnInvincibilityFrames > 0) {
            onSpawnInvincibilityFrames--;
        }
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

    void OnTriggerEnter2D(Collider2D c) {
        if     (c.sharedMaterial.name.Equals("BatHitBox")){
                Batted(c.gameObject);
        }
        if (onSpawnInvincibilityFrames <= 0) {
            if     (c.sharedMaterial.name.Equals("DrillHitBox")){
                    Breaked(c.gameObject);
            }
            if (c.sharedMaterial.name.Equals("Explosion")) {
                Exploded(c.gameObject);
            }
        }
    }

    void Batted(GameObject batter) {
        this.GetComponent<Rigidbody2D> ().AddForce ( 3000 * batter.transform.up);
    }

    void Breaked(GameObject breaker){
        if (this.size != Size.small) {
            this.size -= 1;
            float angle = 2 * Mathf.PI / numChildsSpawnedOnBreak;
            this.transform.localScale /= Mathf.Sqrt(numChildsSpawnedOnBreak);

            float distance = Mathf.Sqrt(Mathf.Pow(this.GetComponent<SpriteRenderer> ().bounds.extents.x,2) * 2);
            for (int i = 1;i<=numChildsSpawnedOnBreak;i++){
                Vector3 newpos = new Vector3(this.transform.position.x + Mathf.Cos(i * angle) * distance, this.transform.position.y + Mathf.Sin(i * angle) * distance, 0);
                GameObject newthing = Instantiate(this.gameObject, newpos, Quaternion.AngleAxis (angle * (i) / Mathf.PI * 180 - 90, this.transform.forward)) as GameObject;
                newthing.GetComponent<Rigidbody2D>().velocity = Quaternion.AngleAxis(angle * (i) / Mathf.PI * 180 - 90, Vector3.forward) * rb.velocity;
                newthing.GetComponent<Rigidbody2D>().mass = this.rb.mass / (numChildsSpawnedOnBreak + 1);

            }
        }
        Destroy(gameObject);
    }

    void Exploded(GameObject exploder) {
        if (this.size != Size.small) {
            this.size -= 1;
            float angle = 2 * Mathf.PI / numChildsSpawnedOnBreak;
            this.transform.localScale /= Mathf.Sqrt(numChildsSpawnedOnBreak);

            float distance = Mathf.Sqrt(Mathf.Pow(this.GetComponent<SpriteRenderer> ().bounds.extents.x,2) * 2);
            for (int i = 1;i<=numChildsSpawnedOnBreak;i++){
                Vector3 newpos = new Vector3(this.transform.position.x + Mathf.Cos(i * angle) * distance, this.transform.position.y + Mathf.Sin(i * angle) * distance, 0);
                GameObject newthing = Instantiate(this.gameObject, newpos, Quaternion.AngleAxis (angle * (i) / Mathf.PI * 180 - 90, this.transform.forward)) as GameObject;
                newthing.GetComponent<Rigidbody2D>().AddForce (Quaternion.AngleAxis (angle * (i) / Mathf.PI * 180 - 90, this.transform.forward)*(this.transform.position - exploder.transform.position)*80);
                newthing.GetComponent<Rigidbody2D>().mass = this.rb.mass / (1 + numChildsSpawnedOnBreak);

            }
        }
        Destroy(gameObject);
    }
}
