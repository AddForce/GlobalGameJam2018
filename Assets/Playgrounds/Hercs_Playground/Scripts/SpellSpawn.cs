using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawn : MonoBehaviour {

    
    [SerializeField] private float force;
    [SerializeField] private GameObject fireBall;


    //will require refining for multiple spells
    private float cooldown;
    private bool canCast;

    //private GameObject firecast;
    //for further expansion:
    private GameObject [] spells;
    private int grimoire = 2; // for spell library
    private int spellIndex;

    //fireball casting point
    private Transform spawnPoint;

    void Awake() {
        spells = new GameObject[grimoire];
        cooldown = 1.5f;
        canCast = true;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        
        //spells = GameObject.FindGameObjectsWithTag("Spell");

        //Remove/Refine later
        //spellIndex = 0;
    }

    void Start(){
        spells[0] = GameObject.Find("FireSpell");
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Space) && canCast) {
            canCast = false;
            GameObject fireSpawn = Instantiate(fireBall, spawnPoint.position, spawnPoint.rotation) as GameObject;
            fireSpawn.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * force);
            spells[0].GetComponent<Animator>().SetTrigger("IsActive");
            StartCoroutine(Cooldown());
        }
	}

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(cooldown);
        canCast = true;
    }
}
