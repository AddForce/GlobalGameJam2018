using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawn : MonoBehaviour {

    
    [SerializeField] private float force;
    [SerializeField] private GameObject fireBall;

    private Transform spawnPoint;

    void Awake() {
        spawnPoint = GameObject.Find("SpawnPoint").transform;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject fireSpawn = Instantiate(fireBall, spawnPoint.position, spawnPoint.rotation) as GameObject;
            fireSpawn.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * force);
        }
	}
}
