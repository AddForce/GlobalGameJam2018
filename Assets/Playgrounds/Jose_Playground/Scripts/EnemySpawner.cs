using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public Enemy[] enemyPrefabs;
	public float spawnPerSecond = 1f;

	List<Enemy> enemies;

	// Use this for initialization
	void Start () {
		enemies = new List<Enemy> ();
		InvokeRepeating ("SpawnEnemy", 0f, spawnPerSecond);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnEnemy(){
		Enemy enemy = enemyPrefabs [Random.Range (0, enemyPrefabs.Length)];
		enemies.Add(Instantiate(enemy, transform.position, transform.rotation));
	}
}
