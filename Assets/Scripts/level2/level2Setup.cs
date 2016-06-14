using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class level2Setup : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        gameManager.reset();
        observer.Inst().addListener(observer.events.objectiveComplete, onReachedKills);
    }

    void onReachedKills(List<object> o)
    {
        StartCoroutine(waitABit());
    }

    IEnumerator waitABit()
    {
        Application.LoadLevelAdditiveAsync("levelComplete");
        yield return new WaitForSeconds(3f);
        Application.LoadLevel("mainMenu");
    }
}
