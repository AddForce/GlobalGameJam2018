using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManaMan : MonoBehaviour {
    private int maxMana = 101;
    private int internalMana = 100;
    public int rechargeInterval = 1;
    public Image manaBar;


    public void castMe(bool isCasting, System.Action<bool> cb) {
        bool isDepleted = false;
            cb(isDepleted);
        if (isCasting) StartCoroutine(deplete(isCasting));
        else StopCoroutine(deplete(isCasting));
    
    }

    public void castSpell(int cost) {
        if (cost > internalMana) {
            StartCoroutine(reCharge(cost, () => {
                internalMana -= cost;
            }
        ));
        } else {
            // internalMana -= cost;
            float fill = internalMana / maxMana;
            print("fill " + fill);
            manaBar.fillAmount = internalMana / maxMana;
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

    private IEnumerator deplete(bool isCasting) {
        if (internalMana >= 0 && isCasting) {
            print(internalMana);
            yield return new WaitForSeconds(rechargeInterval);
            internalMana -= 1;


            
        }
    }

    // Use this for initialization
    void Start() {


    }

    // Update is called once per frame
    void Update() {

    }
}
