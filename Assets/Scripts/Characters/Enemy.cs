using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public int maxHealth = 10;
	public float startingInfectionLevel = 0.5f;
	public float attackSpeed = 1f;
	public int attackPower = 10;
	public Lifebar lifebar;
	public Lifebar infectionBar;

	int health;
	float infectionLevel;
	NavMeshAgent agent;
	Base targetBase;
	float attackTimer;
	bool healed = false;
	bool dead = false;
	float deathTime = 1.5f;

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
		if (HasEndedMoving ()) {
			OnTargetArrival ();
		}
	}

	void OnTargetArrival(){
		if (healed) {
			Destroy (gameObject);
		} else if(targetBase != null){
			AttackBase ();
		}
	}

	void AttackBase(){
		if (attackTimer == 0f) {
			targetBase.Damage (attackPower);
			StartCoroutine (refreshAttackRate ());
		}
	}

	public void DealDamage(int amount){
		health -= amount;
		lifebar.SetPerc ((float)health / (float)maxHealth);
		if(health <= 0 && !dead){
			Die ();
		}
	}

	public void Heal(float perc){
		infectionLevel -= perc;
		infectionBar.SetPerc (infectionLevel);
		if (infectionLevel <= 0f && !healed) {
			OnFullyHealed ();
		}
	}

	void Die(){
		dead = true;
		agent.isStopped = true;
		StartCoroutine (StartDeath ());
	}

	IEnumerator StartDeath(){
		Debug.Log ("Started Death");
		Material mat = GetComponent<MeshRenderer> ().material;

		float t = 1.0f;
		float timer = 0.0f;

		while (timer < deathTime) {
			timer += Time.deltaTime;
			t = 1.0f - (timer / deathTime);
			mat.color = new Color (mat.color.r, mat.color.g, mat.color.b, t);
			yield return null;
		}
		Destroy (gameObject);
	}

	void OnFullyHealed(){
		GameObject exit = GameObject.FindGameObjectWithTag ("ExitPoint");
		agent.SetDestination (exit.transform.position);
		healed = true;
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
