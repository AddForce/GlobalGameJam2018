using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaMan : MonoBehaviour {
    private float internalMana = 100;
    public float rechargeInterval = 1;

    public void castSpell(int cost) {
        if (cost > internalMana) {
            StartCoroutine(reCharge(cost, () => {
                internalMana -= cost;
            }
        ));
        } else {
            internalMana -= cost;
            print(internalMana);
        }
    }

    private IEnumerator reCharge(int cost, System.Action cb) {
        Debug.Log("Waiting for mana to refill...");
        StartCoroutine(refill());
        yield return new WaitUntil(() => internalMana > cost);
        Debug.Log("Now you can use");
        cb();
    }

    private IEnumerator refill() {
        while (internalMana <= 100) {
            yield return new WaitForSeconds(rechargeInterval);
            internalMana += 1;
        }
    }



    // Use this for initialization
    void Start() {


    }

    // Update is called once per frame
    void Update() {

    }
}
