using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour {

    [SerializeField] float growthRate;
    [SerializeField] float maxSize;
    [SerializeField] float alphaReduce;

    public static float maxDamage = 5; //we setting damages in terms of floats or ints?
    public static float maxMag = 27; //empirical value, based on the fireball's expansion rate and lifetime

    public static int manaDepletion = 5;

    //Damage may require the observer pattern. The fireball only "broadcasts" the message, while the enemies ("listeners") take damage

    private Color emission;
    private GameObject trail;

    void Awake() {
        this.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        emission = this.gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
        trail = GameObject.Find("FireTrail");
    }

    void OnCollisionEnter(Collision other) {
        //insert a CompareTag if to affect specific characters in specific ways
        //insert a health depletion rate

        //Careful! Requires tags to be properly set...
        if (other.gameObject.CompareTag("Wall")) Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        if (other.gameObject.CompareTag("Ground")) Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());

        //stops fireball
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //Deactivates trail
        trail.SetActive(false);

        //makes it grow
        InvokeRepeating("Growth", 0.0f, 0.01f);
    }

    void Growth() {
        //makes it grow
        this.gameObject.transform.localScale += Vector3.one * growthRate;
        
        //fades fireball
        this.gameObject.GetComponent<Renderer>().material.color -= new Color(0.0f, 0.0f, 0.0f, alphaReduce);
        emission -= new Color(alphaReduce, alphaReduce/2.0f, 0.0f, alphaReduce);
        this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", emission);

        //destroys fireball if transparency is below zero
        if (this.gameObject.GetComponent<Renderer>().material.color.a <= 0) Destroy(this.gameObject);
        //Debug.Log(this.gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor").ToString());
    }
}
