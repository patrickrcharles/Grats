using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class pauseFunctions : MonoBehaviour {
    
    public void Start()
    {
        Time.timeScale = 0f;
    }

	public void toMenu()
    {
        Application.LoadLevel("mainMenu");
    }

    public void showOptions()
    {
        Application.LoadLevelAdditiveAsync("optionsMenu");
        Application.UnloadLevel("pauseMenu");
    }

    public void restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void returnToGame()
    {
        Time.timeScale = 1f;
        observer.Inst().invokeAction(observer.events.unpaused, null);
        Application.UnloadLevel("pauseMenu");
    }

    public void sensitivityChanged(Scrollbar bar)
    {
        List<object> o = new List<object>();
        PlayerPrefs.SetFloat("sensitivity", bar.value);
        float adjusted = (bar.value + .2f) * 200;
        o.Add(adjusted);
        observer.Inst().invokeAction(observer.events.sensitivityChange, o);
    }

    public void returnToPause()
    {
        PlayerPrefs.Save();
        Application.LoadLevelAdditiveAsync("pauseMenu");
        Application.UnloadLevel("optionsMenu");
    }
}
