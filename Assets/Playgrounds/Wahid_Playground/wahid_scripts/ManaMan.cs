using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ManaMan : MonoBehaviour {
    private int maxMana = 101;
    private int internalMana = 100;
    public float rechargeInterval = 1;
    private float depleteInterval = 1;
    public Image manaBar;
    bool isDepleted = false;

    public void castMe(bool isCasting, System.Action<bool> cb) {

        

        cb(isDepleted);

        if (isCasting) StartCoroutine(deplete());
        else StopCoroutine(deplete());    
    }

    public void castSpell(int cost) {
        if (cost > internalMana) {
            StartCoroutine(reCharge(cost, () => {
                internalMana -= cost;
            }
        ));
        } else {
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

    private IEnumerator deplete() {
        if (internalMana >= 0) {
            yield return new WaitForSeconds(depleteInterval);
            internalMana -= 1;
            print(internalMana);
        }
    }

    // Use this for initialization
    void Start() {


    }

    // Update is called once per frame
    void Update() {

    }
}
