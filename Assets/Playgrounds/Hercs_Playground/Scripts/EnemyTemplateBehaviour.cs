using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTemplateBehaviour : MonoBehaviour {

    private float maxHealth;
    public float health;

    void Awake(){
        maxHealth = 100;
        health = 10;
    }

    void Update() {

        if (health >= maxHealth) health = maxHealth;
        if (health <= 0) Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision other) {

        GameObject spell = GameObject.Find("FireBall");

        if (spell != null) {

            Vector3 distance = other.gameObject.transform.position - this.gameObject.transform.position;

            float linDist = distance.magnitude;

            //distance magnitude from explosion times the maxDamage divided by the MaxMagnitude
            //THIS MATH IS WRONG!!!
            health -= linDist * FireballBehaviour.maxDamage / FireballBehaviour.maxMag;
            
            Debug.Log(this.gameObject.ToString() + ' ' + health.ToString());
        } 
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Healing") InvokeRepeating("Heal", 0.0f, HealBehaviour.healRate);
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "Healing") CancelInvoke("Heal");
    }

    void Heal() {
        health += HealBehaviour.healFactor;
    }
}
