using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpawn : MonoBehaviour {
    [SerializeField]
    private float force;
    [SerializeField]
    private GameObject fireBall; //stores prefab

    private ManaMan manaManager;
    //will require refining for multiple spells
    [SerializeField]
    private float cooldown = 1.5f;
    private bool canCast = true;

    private bool isCasting = false;

    //[SerializeField] private GameObject[] spells;
    [SerializeField]
    private GameObject healingSpell;
    [SerializeField]
    private GameObject fireSpell;

    //fireball casting point
    private Transform spawnPoint;

    //our current spell!

    delegate void spellDelegate();
    spellDelegate curSpell;

    //movementscript here to stop the char when casting heal
    MovementScript move;

    void Awake() {
        move = gameObject.GetComponent<MovementScript>();
        manaManager = gameObject.GetComponent<ManaMan>();
        foreach (Transform child in this.transform) if (child.CompareTag("SpawnPoint")) { spawnPoint = child; }
        curSpell = CheckHeal;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isCasting) {
            curSpell = CheckHeal;
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && !isCasting) { curSpell = ShootFireball; }

        curSpell();

        //    //we pass is Casting which is when you hit the space key;
        //    //is depleted should be false for us to keep the heal going, other wise itll just do nothing
        //    manaManager.castMe(isCasting, (isDepleted) => {
        //        if (isDepleted) {
        //            print("we have depleted our mana");
        //        } else {
        //            CheckHeal();
        //        }
        //    });
        //} else if (curSpell == 1 && canCast) {
        //    if (Input.GetKeyDown(KeyCode.Space)) {
        //        manaManager.castMe(canCast, (isDepleted) => {
        //            ShootFireball();
        //            Debug.Log(isDepleted.ToString());
        //        });
        //    }
        //}
    }

    void CheckHeal() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            isCasting = true;
            healingSpell.gameObject.GetComponent<Animator>().SetTrigger("WasCalled");
            move.stopMove();
        }

        if (Input.GetKey(KeyCode.Space)) {
            canCast = false; //added for safety
                             //spells[curSpell].gameObject.GetComponent<Animator>().SetTrigger("WasCalled");
            healingSpell.gameObject.GetComponent<Animator>().SetBool("StillWorking", true);//could be unnecessary, depending on how it's coded
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            isCasting = false;
            canCast = true;
            healingSpell.gameObject.GetComponent<Animator>().SetBool("StillWorking", false);
            move.startMove();
        }
    }

    void ShootFireball() {
        if (canCast && Input.GetKeyDown(KeyCode.Space)) {
            canCast = false;
            GameObject fireSpawn = Instantiate(fireBall, spawnPoint.position, spawnPoint.rotation) as GameObject;
            fireSpawn.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * force);
            fireSpell.gameObject.GetComponent<Animator>().SetTrigger("WasCalled");
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(cooldown);
        canCast = true;
    }
}
