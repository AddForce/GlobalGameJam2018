using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour {

	public int maxHealth = 100;

	int health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		GameManager.instance.AddBase ();
	}

	void Damage(int d){
		health -= d;
		if (d <= 0) {
			Die ();
		}
	}

	void Die(){
		GameManager.instance.RemoveBase ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
