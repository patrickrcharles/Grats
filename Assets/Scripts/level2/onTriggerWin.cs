using UnityEngine;
using System.Collections;

public class onTriggerWin : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
            observer.Inst().invokeAction(observer.events.objectiveComplete, null);
    }
}
