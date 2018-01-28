using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class ManaMan : MonoBehaviour {
    private float maxMana = 100;
    private float curMana = 100;
    private bool manaCurrentlyUsed = false;
    private spellType castType;

    private bool isDepleted = false;
    private bool fullyCharged = true;


    private float depletionSpeed;
    private float changePerSecond; // modify the total, every second

    public Image manaBar;
    public float timeToChange = 15; // the total time myValue will take to go from max to min

    public float regenSpeed = 0.1f;

    public enum spellType { None, Continous, OneShot };

    void Awake() {
        castType = spellType.None;
        changePerSecond = (0 - maxMana) / timeToChange;
    }

    void FixedUpdate() {
        regen();
    }


    public void castMe(System.Action<bool> cb, float m_dSpeed, spellType m_type) {
        castType = m_type;
        depletionSpeed = m_dSpeed;
        cb(isDepleted);
        if (castType == spellType.Continous) {
            manaCurrentlyUsed = true;
            deplete(depletionSpeed);
        } else if (castType == spellType.OneShot) {
            manaCurrentlyUsed = true;
            onShot(depletionSpeed);
        } else {
            manaCurrentlyUsed = false;
        }
    }

    public void setManaToRegen() {
        manaCurrentlyUsed = false;
    }


    private void deplete(float depletionSpeed) {//for the cast mana method
        if (!Mathf.Approximately(curMana, 0)) {
            fullyCharged = false;
            curMana = Mathf.Clamp(curMana + changePerSecond * Time.deltaTime * depletionSpeed, 0, maxMana);
            manaBar.fillAmount = curMana / maxMana;
        } else {
            isDepleted = true;
            print("depleted on continu");
        }
    }

    private void onShot(float subtractBy) {
        if (!Mathf.Approximately(curMana, 0) && curMana > subtractBy) {
            fullyCharged = false;
            curMana -= subtractBy;
            manaBar.fillAmount = curMana / maxMana;
        } else {
           // isDepleted = true;
            manaCurrentlyUsed = false;
            print("depleted on on shot");
        }

    }

    private void regen() {//for the cast mana method 
        if (!manaCurrentlyUsed && (!Mathf.Approximately(curMana, maxMana) || curMana <= maxMana)) {
            curMana = Mathf.Clamp(curMana + changePerSecond * Time.deltaTime * -regenSpeed, 0, maxMana);
            //manaBar.fillAmount = curMana / maxMana;
        } else if ((Mathf.Approximately(curMana, maxMana) || curMana >= maxMana)) {
            fullyCharged = true;
            print("we gucci " + fullyCharged);
        }
    }
    //private IEnumerator reCharge(int cost, System.Action cb) {
    //    Debug.Log("Waiting for mana to refill...");
    //    StartCoroutine(refill());
    //    yield return new WaitUntil(() => curMana > cost);
    //    Debug.Log("Now you can use");
    //    cb();
    //}

    //private IEnumerator deplete() {
    //    if (curMana >= 0) {
    //        yield return new WaitForSeconds(depleteInterval);
    //        curMana -= 1;
    //        print(curMana);
    //    }
    //}
}
