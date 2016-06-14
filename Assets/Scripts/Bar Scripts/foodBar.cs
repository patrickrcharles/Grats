using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class foodBar : MonoBehaviour
{

    Text text;
    public Image image;
    public int ToNextLevel = 28000;
    List<object> messageParams;

    Image fill;

    int meals = 0;

    // Use this for initialization
    void Start()
    {
        messageParams = new List<object>();
        messageParams.Add("food count");
        text = GetComponentInChildren<Text>();
        fill = transform.FindChild("Image").GetComponent<Image>();
        observer.Inst().addListener(observer.events.eat, onEat);
        UpdateText();
    }

    void onEat(List<object> a)
    {
        meals++;
        if (meals == ToNextLevel)
            observer.Inst().invokeAction(observer.events.objectiveComplete, messageParams);
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = meals.ToString() + " / " + ToNextLevel.ToString();
        float fillAmount = (float)meals / (float)ToNextLevel;
        fill.fillAmount = fillAmount;
    }
}
