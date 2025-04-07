using UnityEngine;

public class WorldState
{
    public string name;
    public bool[] conditionValue;
    public bool[] conditionMask;

    public WorldState()
    {
        conditionValue = new bool[32];
        conditionMask = new bool[32];
    }

    public void SetCondition(int index, bool value)
    {
        if (index >= 0 && index < 32)
        {
            conditionValue[index] = value;
            conditionMask[index] = true;
        }
    }

    public void SetCondition(GPPlanner planner, string conditionName, bool value)
    {
        SetCondition(planner.GetConditionIndex(conditionName), value);
    }

    public bool Match(WorldState goalCondition)
    {
        for (int i = 0; i < 32; i++)
        {
            if (conditionMask[i] && goalCondition.conditionMask[i] &&
                conditionValue[i] != goalCondition.conditionValue[i])
            {
                return false;
            }
        }

        return true;
    }

    public WorldState Clone()
    {
        WorldState clone = new WorldState();
        for (int i = 0; i < 32; i++)
        {
            clone.conditionValue[i] = conditionValue[i];
            clone.conditionMask[i] = conditionMask[i];
        }

        return clone;
    }

    public void Act(WorldState condition)
    {
        for (int i = 0; i < 32; i++)
        {
            conditionMask[i] = conditionMask[i] || condition.conditionMask[i];
            if (condition.conditionMask[i])
            {
                conditionValue[i] = condition.conditionValue[i];
            }
        }
    }
    
    public bool Equal(WorldState condition)
    {
        for (int i = 0; i < 32; i++)
        {
            if (conditionValue[i] != condition.conditionValue[i])
            {
                return false;
            }
        }

        return true;
    }
    
    public int Heuristic(WorldState goal)
    {
        int distance = 0;
        for (int i = 0; i < 32; i++)
        {
            if (goal.conditionMask[i] && conditionValue[i] != goal.conditionValue[i])
            {
                distance++;
            }
        }

        return distance;
    }
}
