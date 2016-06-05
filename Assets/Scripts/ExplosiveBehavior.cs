using UnityEngine;
using System.Collections;

public class ExplosiveBehavior : MonoBehaviour
{
    public int detonateTimer = 100;
    public GameObject explosion;
    // Use this for initialization
    void Start ()
    {
    }

    void explode ()
    {
        Instantiate (explosion, this.transform.position, this.transform.rotation);
        Destroy (gameObject);
    }

    void OnTriggerEnter2D (Collider2D c)
    {
        if (c.sharedMaterial.name.Equals ("BatHitBox")) {
            this.GetComponent<Rigidbody2D> ().AddForce (6000 * c.gameObject.transform.up);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (detonateTimer > 0) {
            detonateTimer--;
        } else {
            explode ();
        }
    }
}
