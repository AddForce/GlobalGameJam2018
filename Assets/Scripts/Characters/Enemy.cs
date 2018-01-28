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

	public AudioClip healedSound;

	int health;
	float infectionLevel;
	NavMeshAgent agent;
	Base targetBase;
	float attackTimer;
	bool healed = false;
	bool dead = false;
	float deathTime = 1.5f;
	Animator anim;
	bool attacking;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
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
			return true; 
		}
		return false;
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat ("speed", agent.speed);
		attacking = false;
		if (HasEndedMoving ()) {
			OnTargetArrival ();
		}
		anim.SetBool ("attacking", attacking);
	}

	void OnTargetArrival(){
		if (healed) {
			Destroy (gameObject);
		} else if(targetBase != null){
			attacking = true;
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
		anim.SetTrigger ("dead");
		GameManager.instance.EnemyKill ();
		StartCoroutine (StartDeath ());
	}

	IEnumerator StartDeath(){
		SpriteRenderer rd = anim.gameObject.GetComponent<SpriteRenderer> ();

		float t = 1.0f;
		float timer = 0.0f;

		while (timer < deathTime) {
			timer += Time.deltaTime;
			t = 1.0f - (timer / deathTime);
			rd.color = new Color (rd.color.r,rd.color.g, rd.color.b, t);
			yield return null;
		}
		Destroy (gameObject);
	}

	void OnFullyHealed(){
		SoundManager.instance.PlaySFX (healedSound);
		GameObject[] exits = GameObject.FindGameObjectsWithTag ("ExitPoint");
		GameObject exit = exits [0];

		for (int i = 1; i < exits.Length; ++i) {
			if (Vector3.Distance (exit.transform.position, transform.position) > Vector3.Distance (exits [i].transform.position, transform.position)) {
				exit = exits [i];
			}
		}

		agent.SetDestination (exit.transform.position);
		agent.speed *= 10.0f;
		agent.autoBraking = true;
		agent.stoppingDistance *= 1.5f;
		SpriteRenderer rd = anim.gameObject.GetComponent<SpriteRenderer> ();
		rd.color = Color.green;
		healed = true;
		GameManager.instance.EnemyHeal ();
	}

	IEnumerator refreshAttackRate(){
		attackTimer = attackSpeed;
		while (attackTimer > 0f) {
			attackTimer -= Time.deltaTime;
			yield return null;
		}

		attackTimer = 0f;
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Spell") {
			Debug.Log ("Collided with spell");
			if (other.gameObject.name == "Healing") {
				Heal (HealBehaviour.healFactor);
			}
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Fireball") {
			DealDamage ((int) FireballBehaviour.maxDamage);
		}
	}
}
