using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class loadOptions : MonoBehaviour {

    [SerializeField]
    Scrollbar bar;

	// Use this for initialization
	void Start () {
        bar.value = PlayerPrefs.GetFloat("sensitivity", .25f);
	}
}
