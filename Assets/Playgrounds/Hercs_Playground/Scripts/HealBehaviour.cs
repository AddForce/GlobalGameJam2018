using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBehaviour : MonoBehaviour {

    public static float healFactor = 5;
    public static float healRate = 0.5f;

    /*
     * We may require a different pattern here. Probably the observer.
     * Something along the lines of "this healing spell broadcasts the healing"
     * only enemies who are colliding with it wil "listen" to the message.
     * Therefore, even the code below may be rendered useless...
     */
     
    void OnTriggerEnter(Collider other) {


        //Requires tagging!
        if (other.gameObject.CompareTag("Enemy")) {
            InvokeRepeating("Heal", 0.0f, healRate);
        }
    }

    void OnTriggerExit(Collider other){

        //See note above
        if (other.gameObject.CompareTag("Enemy")) {
            CancelInvoke("Heal"); //left the string name for debugging purposes
        }
    }

    void Heal() {
        //Heal Code. Something along the lines of 

        //foreach enemy in Enemies
    }
}
