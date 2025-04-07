using System;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu]
public class CurrentWorld : ScriptableObject
{
    [ReorderableList] public WorldAction[] actions = new WorldAction[0];
    [ReorderableList] public WorldGoal[] goals = new WorldGoal[0];
    [ReorderableList] public string[] conditions = new string[0];

    public string GetConditionName(string conditionName)
    {
        int index = Array.FindIndex(conditions, x => x == conditionName);
        if (index >= 0 && index < conditions.Length)
        {
            return conditions[index];
        }

        return null;
    }
}

[Serializable]
public class WorldGoal
{
    public string name;
    public WorldCondition[] preConditions;
}

[Serializable]
public class WorldAction
{
    public string name;
    public int cost;
    public WorldCondition[] preConditions;
    public WorldCondition[] postConditions;
}

[Serializable]
public struct WorldCondition
{
    public string name;
    public bool value;
}
