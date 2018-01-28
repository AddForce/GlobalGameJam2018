using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

    public int maxHealth = 100;
    public ParticleSystem deathParticles;
    public Lifebar lifebar;

    int health;

    private Light healthLight;

    // Use this for initialization
    void Start() {
        healthLight = GetComponentInChildren<Light>();
        health = maxHealth;
        GameManager.instance.AddBase();
    }

    public void Damage(int d) {
        health -= d;
        float perc = (float)health / (float)maxHealth;
        lifebar.SetPerc(perc);
        // Debug.Log(Map(0, 10, 0, 1024, 500));
        print(perc);
        healthLight.intensity = Map(0, 1, 0, 8, perc);
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        GameManager.instance.RemoveBase();
        deathParticles.Play();
    }

    // Update is called once per frame
    void Update() {

    }

    public float Map(float from, float to, float from2, float to2, float value) {
        if (value <= from2) {
            return from;
        } else if (value >= to2) {
            return to;
        } else {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }
}