using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public int maxHealth = 10;
	public float startingInfectionLevel = 0.5f;

	int health;
	float infectionLevel;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		infectionLevel = startingInfectionLevel;
		agent = GetComponent<NavMeshAgent> ();

		Base[] bases = FindObjectsOfType<Base> ();
		float d = -1;

		for (int i = 0; i < bases.Length; ++i) {
			float dist = Vector3.Distance (bases [0].transform.position, transform.position);
			if (d == -1 || dist < d) {
				d = dist;
				agent.SetDestination (bases [0].transform.position);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
