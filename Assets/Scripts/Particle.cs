using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {
    public int framesAlive;
    public float decayRate;
    SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
        Color color = renderer.color;
        Vector3 scale = transform.localScale;
        scale *= decayRate;
        color.a *= decayRate;
        //renderer.color = color;
        transform.localScale = scale;
        framesAlive--;
        if (framesAlive < 0) {
            Destroy (gameObject);
        }
	}
}
