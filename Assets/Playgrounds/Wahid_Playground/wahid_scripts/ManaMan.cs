using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof (Image))]
public class ManaMan : MonoBehaviour {
    private float maxMana = 101;
    private float curMana = 100;
    public float decreaseByThisEverySec = 1;

    public float rechargeInterval = 1;
    public float depleteInterval = 1;

    public Image manaBar;
    bool isDepleted = false;

    float changePerSecond; // modify the total, every second
    public float timeToChange = 15; // the total time myValue will take to go from max to min

    public void castMe(System.Action<bool> cb) {
 
    }

    void Awake() {
        changePerSecond = (0 - maxMana) / timeToChange;
    }

    void FixedUpdate() {
        deplete();
        print(curMana);
    }

    private void deplete() {//for the cast mana method
        if (curMana >= 0) {
            curMana = Mathf.Clamp(curMana + changePerSecond * Time.deltaTime, 0, maxMana);
            manaBar.fillAmount = curMana / maxMana;
        }

    }

    public void castSpell(int cost) {
        if (cost > curMana) {
            StartCoroutine(reCharge(cost, () => {
                curMana -= cost;
            }
        ));
        } else {
            float fill = curMana / maxMana;
            print("fill " + fill);
            manaBar.fillAmount = curMana / maxMana;
            print(curMana);
        }
    }

    private IEnumerator reCharge(int cost, System.Action cb) {
        Debug.Log("Waiting for mana to refill...");
        StartCoroutine(refill());
        yield return new WaitUntil(() => curMana > cost);
        Debug.Log("Now you can use");
        cb();
    }

    private IEnumerator refill() {
        while (curMana <= 100) {
            yield return new WaitForSeconds(rechargeInterval);
            curMana += 1;
        }
    }

    //private IEnumerator deplete() {
    //    if (curMana >= 0) {
    //        yield return new WaitForSeconds(depleteInterval);
    //        curMana -= 1;
    //        print(curMana);
    //    }
    //}

    // Use this for initialization
    void Start() {


    }


}
