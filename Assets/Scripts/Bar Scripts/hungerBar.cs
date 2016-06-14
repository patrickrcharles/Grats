using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class hungerBar: MonoBehaviour {

	Text text;
	public Image image;
	public int ToNextLevel = 28000;

    gameManager.status playerStatus;
    Image fill;

	// Use this for initialization
	void Start () 
	{
        playerStatus = gameManager.inst().PlayerStatus;
		text = GetComponentInChildren<Text> ();
        fill = transform.FindChild("Image").GetComponent<Image>();
    }
	
	void Update()
	{
		UpdateText ();
	}
	
	
	
	public void UpdateText()
	{
		text.text = playerStatus.hunger.ToString () + " / " + ToNextLevel.ToString();
        float fillAmount = (float)playerStatus.hunger / (float)ToNextLevel;
        fill.fillAmount = fillAmount;
	}
}
