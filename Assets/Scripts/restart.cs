using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class restart : MonoBehaviour {

    // Use this for initialization
    Text secondText;

	void Start () {
        secondText = GameObject.Find("secondText").GetComponent<Text>();
        StartCoroutine(restartLevel());
	}

    IEnumerator restartLevel()
    {
        for (int i = 4; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f*Time.timeScale);
            secondText.text = i + " Seconds";
        }
        Application.LoadLevel(Application.loadedLevel);
    }
}
