using System;
using UnityEditor;
using UnityEngine;

public class GPAgentData
{
    public GPState[] states;
    public GPState defaultState;
    public GPState currentState;
    public WorldState currentGoal;
    public GPPlanner planner;
    public GPPlan currentPlan;
    public AgentSense agentSense;
    public WorldState worldState;
    
    public GPAgentData(GameObject agent)
    {
        agentSense = new AgentSense(agent);
        planner = new GPPlanner();
        currentPlan = new GPPlan();
        worldState = new WorldState();
        currentState = null;
        defaultState = null;
        currentGoal = null;
    }

    public void UpdateCurrentState()
    {
        currentState?.UpdateState();
    }

    public void ExecuteBehavior()
    {
        agentSense.GetConditions(this, worldState);
        if (currentState == null)
        {
            ResetCurrentState();
        }
        else
        {
            if (currentState.IsCompleted)
            {
                SetNewState(SelectNewState(worldState));
            }
        }

        foreach (string action in currentPlan.actions)
        {
            Debug.Log(action);
        }
    }

    public string SelectNewState(WorldState worldState)
    {
        string newState = defaultState.name;
        if (currentGoal != null)
        {
            planner.GeneratePlan(ref currentPlan, worldState, currentGoal);
            if (currentPlan.HasPlan && currentPlan.HasActions)
            {
                Debug.Log($"We have a plan");
                string actionName = planner.ReadAction(currentPlan.GetAction(0)).name;
                newState = planner.GetState(actionName);
            }
        }

        return newState;
    }
    
    private void SetNewState(string name)
    {
        if (!string.Equals(currentState.name, name))
        {
            currentState = FindState(name);
            if (currentState != null)
            {
                currentState.Initialize();
            }
            else
            {
                ResetCurrentState();
            }
        }
    }
    
    private void ResetCurrentState()
    {
        currentState = defaultState;
        currentState.Initialize();
    }
    
    public void SetDefaultState(string stateName)
    {
        defaultState = FindState(stateName);
        if (defaultState == null)
        {
            Debug.Log($"Default State is null");
        }
    }

    private GPState FindState(string stateName)
    {
        int index = Array.FindIndex(states, x => x.name.Equals(stateName));
        if (index >= 0 && index < states.Length)
        {
            return states[index];
        }

        return null;
    }

    public void SetGoal(string goalName)
    {
        currentGoal = FindGoal(goalName);
        if (currentGoal == null)
        {
            Debug.Log($"Current Goal not found");
        }
    }

    private WorldState FindGoal(string goalName)
    {
        int index = planner.goals.FindIndex(x => x.name.Equals(goalName));
        if (index >= 0 && index < planner.goals.Count)
        {
            return planner.goals[index];
        }

        return null;
    }
}
