﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawn : MonoBehaviour {
    [SerializeField] private float force;
    [SerializeField] private GameObject fireBall; //stores prefab

    private ManaMan manaManager;
    //will require refining for multiple spells
    [SerializeField] private float cooldown;
    private bool canCast;

    private bool isCasting;

    [SerializeField] private GameObject[] spells;
    private int curSpell;

    //fireball casting point
    private Transform spawnPoint;

    void Awake() {
        manaManager = this.gameObject.GetComponent<ManaMan>();
        cooldown = 1.5f;
        canCast = true;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        isCasting = false;
    }

    void Start() {
        curSpell = 0; //initializing for a default
    }

    // Update is called once per frame
    void Update() {

        //I'd thought of Spell 0 being fireball and 1 being Healing... Unity didn't like that...
        if (Input.GetKeyDown(KeyCode.Alpha1)) curSpell = 0; //healing
        else if (Input.GetKeyDown(KeyCode.Alpha2)) curSpell = 1; //fireball

        if (curSpell >= spells.Length || curSpell < 0) curSpell = 0; //added for safety...

        if (curSpell == 0) {
            //we pass is Casting which is when you hit the space key;
            //is depleted should be false for us to keep the heal going, other wise itll just do nothing
                manaManager.castMe(isCasting, (isDepleted) => {
                    if (isDepleted) {
                        print("we have depleted our mana");
                    } else {
                        CheckHeal();
                    }
                });            
        } else if (curSpell == 1 && canCast) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                manaManager.castMe(canCast, (isDepleted) => {
                    ShootFireball();
                    Debug.Log(isDepleted.ToString());
                });
            }
        }
    }

    void CheckHeal() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            isCasting = true;
            spells[curSpell].gameObject.GetComponent<Animator>().SetTrigger("WasCalled");
        }

        if (Input.GetKey(KeyCode.Space)) {
            canCast = false; //added for safety
                             //spells[curSpell].gameObject.GetComponent<Animator>().SetTrigger("WasCalled");
            spells[curSpell].gameObject.GetComponent<Animator>().SetBool("StillWorking", true);//could be unnecessary, depending on how it's coded
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            isCasting = false;
            canCast = true;
            spells[curSpell].gameObject.GetComponent<Animator>().SetBool("StillWorking", false);
        }
    }

    void ShootFireball() {
        canCast = false;
        GameObject fireSpawn = Instantiate(fireBall, spawnPoint.position, spawnPoint.rotation) as GameObject;
        fireSpawn.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * force);
        spells[curSpell].gameObject.GetComponent<Animator>().SetTrigger("WasCalled");
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(cooldown);
        canCast = true;
    }

}
