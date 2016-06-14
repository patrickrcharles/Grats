using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class killBar : MonoBehaviour
{

    Text text;
    public Image image;
    public int ToNextLevel = 28000;
    List<object> messageParams;
    
    Image fill;

    int kills = 0;

    // Use this for initialization
    void Start()
    {
        messageParams = new List<object>();
        messageParams.Add("kill count");
        text = GetComponentInChildren<Text>();
        fill = transform.FindChild("Image").GetComponent<Image>();
        observer.Inst().addListener(observer.events.NPCDeath, onNPCDeath);
        UpdateText();
    }

    void onNPCDeath(List<object> a)
    {
        kills++;
        if (kills == ToNextLevel)
            observer.Inst().invokeAction(observer.events.objectiveComplete, messageParams);
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = kills.ToString() + " / " + ToNextLevel.ToString();
        float fillAmount = (float)kills / (float)ToNextLevel;
        fill.fillAmount = fillAmount;
    }
}
