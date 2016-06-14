using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tutorialSetup : MonoBehaviour {

    // Use this for initialization
    GameObject c2;

    void Awake()
    {
        c2 = GameObject.Find("Camera2");
        c2.SetActive(false);
        gameManager.reset();
        observer.Inst().addListener(observer.events.objectiveComplete, onReachedKills);
    }

    void onReachedKills(List<object> o)
    {
        StartCoroutine(waitABit());
    }

    IEnumerator waitABit()
    {
        yield return new WaitForSeconds(2f);
        c2.SetActive(true);
        Application.LoadLevelAdditiveAsync("levelComplete");
        yield return new WaitForSeconds(3f);
        Application.LoadLevel("level1");
    }
}

