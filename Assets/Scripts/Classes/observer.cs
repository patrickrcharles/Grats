using System.Collections.Generic;
using System;
using UnityEngine;

public class observer {

    Dictionary<events, List<Action<List<object>>>> methodList;

    public enum events {
        NPCDeath,
        playerDeath,
        objectiveComplete,
        unpaused,
        eat,
        sensitivityChange
    }

    static observer inst;
    public static observer Inst()
    {
        if (inst == null)
            inst = new observer();
        return inst;
    }

    private observer()
    {
        methodList = new Dictionary<events, List<Action<List<object>>>>();
    }

    public void addListener(events e, Action<List<object>> a)
    {
        if (!methodList.ContainsKey(e))
            methodList[e] = new List<Action<List<object>>>();
        methodList[e].Add(a);
    }

    public void invokeAction(events e, List<object> parameters)
    {
        Debug.Log("[Event] " + Enum.GetName(typeof(events), e));
        if (!methodList.ContainsKey(e))
            return;
        foreach (Action<List<object>> a in methodList[e]) {
            a.Invoke(parameters);
        }
    }

    public static void reset()
    {
        inst = new observer();
    }
}
