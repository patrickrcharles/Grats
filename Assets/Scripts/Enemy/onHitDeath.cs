using UnityEngine;
using System.Collections;

public class onHitDeath : MonoBehaviour {

    bool killed = false;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player" && !killed)
        {
            killed = true;
            observer.Inst().invokeAction(observer.events.playerDeath, null);
        }
    }
}
