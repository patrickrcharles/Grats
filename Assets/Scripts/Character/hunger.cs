using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class hunger : MonoBehaviour {

    float maxHunger = 100f;

    [SerializeField]
    float hungerLossPerSecond;

    Text UIText;

    gameManager.status myStatus;

    void Start()
    {
        myStatus = gameManager.inst().PlayerStatus;
        observer.Inst().addListener(observer.events.eat, eat);
        StartCoroutine(getHungry());
    }

    public void eat(List<object> o)
    {
        switch ((string) o[1])
        {
            case "meat":
                myStatus.hunger += 40;
                break;
            case "bread":
                myStatus.hunger += 20;
                break;
            default:
                myStatus.hunger += 20;
                break;
        }
        if (myStatus.hunger > maxHunger)
            myStatus.hunger = maxHunger;
        GameObject.Destroy((GameObject)o[0]);
    }

    IEnumerator getHungry()
    {
        while (myStatus.hunger > 0) // not dead (gm)
        {
            yield return new WaitForSeconds(1f);
            myStatus.hunger -= hungerLossPerSecond;
            if (myStatus.hunger < 0)
                myStatus.hunger = 0;
        }
        observer.Inst().invokeAction(observer.events.playerDeath, null);
    }
}
