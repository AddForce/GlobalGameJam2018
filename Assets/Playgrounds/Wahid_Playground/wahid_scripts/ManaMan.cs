using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaMan : MonoBehaviour
{
    private int internalMana = 100;
    public int rechargeInterval = 1;


    public void castMe(bool canCast, System.Action cb)
    {
        if (canCast)
        {
            cb();
        }
    }

    public void castSpell(int cost)
    {
        if (cost > internalMana)
        {
            StartCoroutine(reCharge(cost, () =>
            {
                internalMana -= cost;
            }
        ));
        }
        else
        {
            internalMana -= cost;
            print(internalMana);
        }
    }

    private IEnumerator reCharge(int cost, System.Action cb)
    {
        Debug.Log("Waiting for mana to refill...");
        StartCoroutine(refill());
        yield return new WaitUntil(() => internalMana > cost);
        Debug.Log("Now you can use");
        cb();
    }

    private IEnumerator refill()
    {
        while (internalMana <= 100)
        {
            yield return new WaitForSeconds(rechargeInterval);
            internalMana += 1;
        }
    }



    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
