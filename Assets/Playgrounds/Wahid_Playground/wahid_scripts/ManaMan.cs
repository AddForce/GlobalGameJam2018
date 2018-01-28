using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class ManaMan : MonoBehaviour {
    private float maxMana = 101;
    private float curMana = 100;

    public Image manaBar;
    bool isDepleted = false;

    float changePerSecond; // modify the total, every second
    public float timeToChange = 15; // the total time myValue will take to go from max to min

    private float depletionSpeed;

    private spellType castType;

    public enum spellType { None, Continous, OneShot };

    public void castMe(System.Action<bool> cb, float m_dSpeed, spellType m_type) {
        castType = m_type;
        depletionSpeed = m_dSpeed;
        cb(isDepleted);
        if (castType == spellType.Continous) {
            deplete(depletionSpeed);
        } else if (castType == spellType.OneShot) {

        }
    }

    void Awake() {
        castType = spellType.None;
        changePerSecond = (0 - maxMana) / timeToChange;
    }

    private void deplete(float depletionSpeed) {//for the cast mana method
        if (!Mathf.Approximately(curMana, 0)) {
            curMana = Mathf.Clamp(curMana + changePerSecond * Time.deltaTime * depletionSpeed, 0, maxMana);
            manaBar.fillAmount = curMana / maxMana;
        } else {
            isDepleted = true;
            print("depleted");
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
