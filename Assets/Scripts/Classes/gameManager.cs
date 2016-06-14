using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class gameManager {

    List<GameObject> enemies;
    List<status> enemyStatuses;

    bool isPaused = false;
   
    GameObject player;

    [SerializeField]
    status playerStatus;

    public status PlayerStatus
    {
        get
        {
            return playerStatus;
        }
    }

    public Vector3 PlayerPosition
    {
        get
        {
            return player.transform.position;
        }
    }

    public class status
    {
        public bool alive;
        public int health;
        public float hunger;

        public status(bool alive, int health, float hunger)
        {
            this.alive = alive;
            this.health = health;
            this.hunger = hunger;
        }
    }

    static gameManager gm;
    public static gameManager inst()
    {
        if (gm == null)
            gm = new gameManager();
        return gm;
    }

    private gameManager()
    {
        Debug.Log("GM Initialized");
        Time.timeScale = 1f;
        getEnemies();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = new status(true, 100, 100);
        observer.reset();
        observer.Inst().addListener(observer.events.playerDeath, onPlayerDeath);
        observer.Inst().addListener(observer.events.unpaused, unpaused);
        Debug.Log("Previous current level = " + PlayerPrefs.GetInt("currentLevel"));
        PlayerPrefs.SetInt("currentLevel", Application.loadedLevel);
        PlayerPrefs.Save();
        Debug.Log("Saving current level as " + PlayerPrefs.GetInt("currentLevel"));
        setSensitivity();
    }

    public void setSensitivity()
    {
        List<object> o = new List<object>();
        o.Add(PlayerPrefs.GetFloat("sensitivity", .25f));
        Debug.Log("Sensitivity set as " + (float)o[0]);
        observer.Inst().invokeAction(observer.events.sensitivityChange, o);
    }

    void onPlayerDeath(List<object> derp)
    {
        Time.timeScale = .1f;
        Application.LoadLevelAdditiveAsync("youDied");
    }

    public void pauseMenu()
    {
        if (!isPaused)
        {
            isPaused = true;
            Application.LoadLevelAdditiveAsync("pauseMenu");
        }
    }

    public void unpaused(List<object> o)
    {
        isPaused = false;
    }

    public static void reset()
    {
        gm = new gameManager();
    }

    private void getEnemies()
    {
        enemies = new List<GameObject>();
        enemyStatuses = new List<status>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy"))
        {
            enemies.Add(go);
            enemyStatuses.Add(new status(true, 100, 0));
        }
    }

    public bool isDead(GameObject go)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == go)
            {
                return enemyStatuses[i].alive;
            }
        }
        return true;
    }

    public status getStatus(GameObject go)
    {
        if (go.tag == "Player")
            return PlayerStatus;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == go)
            {
                return enemyStatuses[i];
            }
        }
        return null;
    }
}
