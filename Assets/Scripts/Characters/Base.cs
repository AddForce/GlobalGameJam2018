using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

	public int maxHealth = 100;
	public ParticleSystem deathParticles;
	public Lifebar lifebar;

	int health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		GameManager.instance.AddBase ();
	}

	public void Damage(int d){
		health -= d;
		lifebar.SetPerc ((float)health / (float)maxHealth);
		if (health <= 0) {
			Die ();
		}
	}

	void Die(){
		GameManager.instance.RemoveBase ();
		deathParticles.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
