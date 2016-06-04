using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public enum Size {small, medium, large};
	public Size size;
	public int numChildsSpawnedOnBreak;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Batted (new GameObject());
		}
		if (Input.GetMouseButtonDown (1)) {
			Breaked (new GameObject());
		}
	}

	void OnCollisionEnter2D(Collision2D c){
		if 	(c.collider.sharedMaterial.name.Equals("fast")){
			Batted(c.gameObject);
		}
		if 	(c.collider.sharedMaterial.name.Equals("strong")){
			Breaked(c.gameObject);
		}
	}

	void Batted(GameObject batter){
		this.GetComponent<Rigidbody2D> ().AddForce ( 5200 * batter.transform.up);
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
			}
		}
		Destroy (gameObject);
	}
}
