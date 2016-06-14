using UnityEngine;
using System.Collections;

public class loadScript : MonoBehaviour {

    public void startGame()
    {
        if (!PlayerPrefs.HasKey("currentLevel"))
            Debug.Log("New Player");
        Application.LoadLevel(PlayerPrefs.GetInt("currentLevel", 1));
    }

    public void loadLevel(string level)
    {
        Application.LoadLevel(level);
    }
}
