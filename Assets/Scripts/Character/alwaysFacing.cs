using UnityEngine;
using System.Collections;

public class alwaysFacing : MonoBehaviour {

    [SerializeField]
    GameObject go;

	// Update is called once per frame
	void Update () {
        transform.LookAt(go.transform);
	}
}
