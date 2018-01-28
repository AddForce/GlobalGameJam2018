using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ManaMan))]
[RequireComponent(typeof(MovementScript))]
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
        curHealStage = healStage.None;
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
    }

    enum healStage { None, Started, Mid, Ended };
    healStage curHealStage;


    private float depletionSpeed = 3;
    void CheckHeal() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            curHealStage = healStage.Started;
            isCasting = true;

        } else if (Input.GetKey(KeyCode.Space)) {
            curHealStage = healStage.Mid;
            canCast = false;
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            curHealStage = healStage.Ended;
            isCasting = false;
            canCast = true;
        }
        //print(curHealStage);
        switch (curHealStage) {
            case healStage.None:
                break;
            case healStage.Started:
                manaManager.castMe((isDepleted) => {
                    print("started heal");
                    healingSpell.gameObject.GetComponent<Animator>().SetTrigger("WasCalled");
                    move.stopMove();
                }, depletionSpeed, ManaMan.spellType.Continous);
                break;
            case healStage.Mid:
                //TODO THIS IS WHERE THE ACTUALL HEALING WILL HAPPEN
                manaManager.castMe((isDepleted) => {
                    if (!isDepleted) {
                        healingSpell.gameObject.GetComponent<Animator>().SetBool("StillWorking", true);//could be unnecessary, depending on how it's coded
                    } else {
                        healingSpell.gameObject.GetComponent<Animator>().SetBool("StillWorking", false);
                        print("uh oh we ran out");
                        //so we are depleted now 
                        if (!move.getMove()) {
                            move.startMove();
                        }
                    }
                }, depletionSpeed, ManaMan.spellType.Continous);
                break;
            case healStage.Ended:
                manaManager.castMe((isDepleted) => {
                    healingSpell.gameObject.GetComponent<Animator>().SetBool("StillWorking", false);
                    move.startMove();
                }, depletionSpeed, ManaMan.spellType.None);
                break;
            default:
                break;
        }
    }

    private float manaUsed = 10;
    void ShootFireball() {
        if (canCast && Input.GetKeyDown(KeyCode.Space)) {
            canCast = false;
            manaManager.castMe((isDepleted) => {
                if (!isDepleted) {
                    GameObject fireSpawn = Instantiate(fireBall, spawnPoint.position, spawnPoint.rotation) as GameObject;
                    fireSpawn.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * force);
                    fireSpell.gameObject.GetComponent<Animator>().SetTrigger("WasCalled");
                    StartCoroutine(Cooldown());
                }
            }, manaUsed, ManaMan.spellType.OneShot);
        }
    }

    IEnumerator Cooldown() {
        yield return new WaitForSeconds(cooldown);
        manaManager.setManaToRegen();
        canCast = true;
    }
}
