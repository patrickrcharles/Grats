using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Infection : MonoBehaviour {

    [SerializeField]
    int infectionAmount = 0;

    [SerializeField]
    int infectionPerSecond;
    [SerializeField]
    int infectionLossPerSecond;

    gameManager.status myStatus;


    void Start()
    {
        myStatus = gameManager.inst().getStatus(transform.parent.gameObject);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "rat" && myStatus.alive)
        {
            StopAllCoroutines();
            StartCoroutine(increaseInfection());
        }
    }

    void OnTriggerExit(Collider c)
    {

        if (c.tag == "rat" && myStatus.alive)
        {
            StopAllCoroutines();
            StartCoroutine(decreaseInfection());
        }
    }

    IEnumerator increaseInfection()
    {
        do
        {
            yield return new WaitForSeconds(1f);
            infectionAmount += infectionPerSecond;
        } while (infectionAmount < 100);
        List<object> temp = new List<object>();
        temp.Add(transform.parent.gameObject.name);
        observer.Inst().invokeAction(observer.events.NPCDeath, temp);
    }

    IEnumerator decreaseInfection()
    {
        while (infectionAmount > 0)
        {
            yield return new WaitForSeconds(1f);
            infectionAmount -= infectionLossPerSecond;
            if (infectionAmount < 0)
                infectionAmount = 0;
        }
    }
}
