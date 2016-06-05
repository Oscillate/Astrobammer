using UnityEngine;
using System.Collections;

public class ExplosionBehavior : MonoBehaviour {
    public int explosiveForceLifeTime = 2;
    public float explosionFadeOutTime = 0.4f;
    private float explosionStartTime;
    // Use this for initialization
    void Start () {
        explosionStartTime = 0;
    }

	// Update is called once per frame
	void Update () {
        if (explosiveForceLifeTime <= 0) {
            this.GetComponent<CircleCollider2D>().enabled = false;
            if (explosionStartTime==0){
                explosionStartTime = Time.time;
            }
            float t = (Time.time - explosionStartTime) / explosionFadeOutTime;

            this.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,Mathf.SmoothStep(1f, 0f, t));
            if (Time.time - explosionStartTime > explosionFadeOutTime) {
                Destroy(gameObject);
            }
        }
        else {
            explosiveForceLifeTime--;
       }
    }
}
