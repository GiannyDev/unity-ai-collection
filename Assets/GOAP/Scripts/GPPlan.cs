using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPPlan
{
    public bool HasPlan { get; set; }
    public bool HasActions => actions.Count > 0;
    
    public List<string> actions;

    public GPPlan()
    {
        actions = new List<string>();
    }

    public void Reset()
    {
        actions.Clear();
    }

    public void Insert(string actionName)
    {
        actions.Insert(0, actionName);
    }

    public string GetAction(int index)
    {
        if (index >= 0 && index < actions.Count)
        {
            return actions[index];
        }

        return null;
    }
}
