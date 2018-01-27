using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour {

    [SerializeField] float growthRate;
    [SerializeField] float maxSize;
    [SerializeField] float alphaReduce;

    void Awake() {
        this.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void OnCollisionEnter(Collision other) {
        //insert a CompareTag if to affect specific characters in specific ways
        //insert a health depletion rate

        //Careful! Requires tags to be properly set...
        if (other.gameObject.CompareTag("Wall")) Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        if (other.gameObject.CompareTag("Ground")) Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());

        //stops fireball
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //makes it grow
        InvokeRepeating("Growth", 0.0f, 0.01f);
    }

    void Growth() {
        this.gameObject.transform.localScale += Vector3.one * growthRate;
        this.gameObject.GetComponent<Renderer>().material.color -= new Color(0.0f, 0.0f, 0.0f, alphaReduce);
        if (this.gameObject.GetComponent<Renderer>().material.color.a <= 0) Destroy(this.gameObject);
        //Debug.Log(Vector3.one.magnitude.ToString());
    }
}
