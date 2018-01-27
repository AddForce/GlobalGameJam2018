using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public int maxHealth = 10;
	public float startingInfectionLevel = 0.5f;
	public float attackSpeed = 1f;
	public int attackPower = 10;

	int health;
	float infectionLevel;
	NavMeshAgent agent;
	Base targetBase;
	float attackTimer;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		infectionLevel = startingInfectionLevel;
		agent = GetComponent<NavMeshAgent> ();

		Base[] bases = FindObjectsOfType<Base> ();
		float d = -1;

		for (int i = 0; i < bases.Length; ++i) {
			float dist = Vector3.Distance (bases [i].transform.position, transform.position);
			if (d == -1 || dist < d) {
				d = dist;
				if (!agent.SetDestination (bases [i].transform.position)) {
					Debug.Log ("Problem setting destination");
				} else {
					targetBase = bases [i];
				}
			}
		}
	}

	bool HasEndedMoving(){
		if (Vector3.Distance (agent.destination, agent.transform.position) <= agent.stoppingDistance) {
			return !agent.hasPath || agent.velocity.sqrMagnitude == 0f; 
		}
		return false;
	}
	
	// Update is called once per frame
	void Update () {
		if (targetBase != null && HasEndedMoving ()) {
			AttackBase ();
		}
	}

	void AttackBase(){
		if (attackTimer == 0f) {
			targetBase.Damage (attackPower);
			StartCoroutine (refreshAttackRate ());
		}
	}

	IEnumerator refreshAttackRate(){
		attackTimer = attackSpeed;
		while (attackTimer > 0f) {
			attackTimer -= Time.deltaTime;
			yield return null;
		}

		attackTimer = 0f;
	}
}
