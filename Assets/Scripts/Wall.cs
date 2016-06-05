using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	public float minForce;
	public float multiplier;
	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D c){
		//Debug.Log (c.name);
		if (c.sharedMaterial == null || !c.sharedMaterial.name.Equals("Wall")){
			Vector3 distance = transform.position - c.bounds.center - new Vector3 (0, c.bounds.extents.y, 0);
			Vector3 force = transform.up * ((Vector3.Project (distance, transform.up).magnitude) * multiplier + minForce);
			c.gameObject.GetComponent<Rigidbody2D> ().AddForce (force);
			Debug.Log ("asda" + Vector3.Project (distance, transform.up).magnitude);
			Debug.Log (force.magnitude);
		}
	}
}
