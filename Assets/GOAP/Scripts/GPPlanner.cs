using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GPPlanner
{
    public List<string> conditions = new List<string>();
    public List<GPAction> actions = new List<GPAction>();
    public List<WorldState> goals = new List<WorldState>();

    public void LoadCurrentWorldInfo(CurrentWorld world)
    {
        for (int i = 0; i < world.actions.Length; i++)
        {
            WorldAction worldAction = world.actions[i];
            GPAction action = ReadAction(worldAction.name);
            action.cost = worldAction.cost;
            
            // Load Pre conditions
            int conditionIndex;
            for (int j = 0; j < worldAction.preConditions.Length; j++)
            {
                conditionIndex = GetConditionIndex(world.GetConditionName(worldAction.preConditions[j].name));
                action.preConditions.SetCondition(conditionIndex, worldAction.preConditions[j].value);
            }

            // Post Conditions
            for (int j = 0; j < worldAction.postConditions.Length; j++)
            {
                conditionIndex = GetConditionIndex(world.GetConditionName(worldAction.postConditions[j].name));
                action.postConditions.SetCondition(conditionIndex, worldAction.postConditions[j].value);
            }
        }

        // Goals
        for (int i = 0; i < world.goals.Length; i++)
        {
            WorldGoal worldGoal = world.goals[i];
            WorldState goal = ReadGoal(worldGoal.name);
            
            for (int j = 0; j < world.goals.Length; j++)
            {
                int index = GetConditionIndex(world.GetConditionName(worldGoal.preConditions[j].name));
                goal.SetCondition(index, worldGoal.preConditions[j].value);
            }
        }
    }

    public void GeneratePlan(ref GPPlan plan, WorldState currentWorld, WorldState goal)
    {
        List<GPNode> openList = new List<GPNode>();
        List<GPNode> closedList = new List<GPNode>();
        
        openList.Add(new GPNode()
        {
            parentConditions = null,
            worldConditions = currentWorld,
            action = "",
            heuristic = currentWorld.Heuristic(goal),
            cost = 0,
            sum = currentWorld.Heuristic(goal)
        });

        GPNode currentNode = null;
        while (openList.Count > 0)
        {
            currentNode = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].sum < currentNode.sum)
                {
                    currentNode = openList[i];
                }
            }
            
            openList.Remove(currentNode);
            if (currentNode.worldConditions.Match(goal))
            {
                // Found plan
                BuildPlan(ref plan, closedList, currentNode);
                plan.HasPlan = true;
                return;
            }
            
            closedList.Add(currentNode);
            List<GPAction> neighbors = GetTransitions(currentNode.worldConditions);
            for (int i = 0; i < neighbors.Count; i++)
            {
                int cost = currentNode.cost + neighbors[i].cost;
                WorldState neighborCondition = currentNode.worldConditions.Clone();
                neighborCondition.Act(neighbors[i].postConditions);

                int openIndex = FindEqual(openList, neighborCondition);
                int closeIndex = FindEqual(closedList, neighborCondition);

                if (openIndex > -1 && cost < openList[openIndex].cost)
                {
                    openList.RemoveAt(openIndex);
                    openIndex = -1;
                }
                
                if (closeIndex > -1 && cost < closedList[closeIndex].cost)
                {
                    closedList.RemoveAt(closeIndex);
                    closeIndex = -1;
                }

                if (openIndex == -1 && closeIndex == -1)
                {
                    openList.Add(new GPNode()
                    {
                        parentConditions = currentNode.worldConditions,
                        worldConditions = neighborCondition,
                        action = neighbors[i].name,
                        heuristic = neighborCondition.Heuristic(goal),
                        cost = cost,
                        sum = cost + neighborCondition.Heuristic(goal)
                    });
                }
            }
        }
        
        // Failed plan
        BuildPlan(ref plan, closedList, currentNode);
        plan.HasPlan = false;
    }

    private void BuildPlan(ref GPPlan plan, List<GPNode> closed, GPNode node)
    {
        plan.Reset();
        GPNode currentNode = node;
        while (currentNode != null && currentNode.parentConditions != null)
        {
            plan.Insert(currentNode.action);
            int index = FindEqual(closed, currentNode.parentConditions);
            currentNode = index == -1 ? closed[0] : closed[index];
        }
    }
    
    #region Actions

    public GPAction ReadAction(string actionName)
    {
        GPAction action = FindAction(actionName);
        if (action == null)
        {
            action = new GPAction(actionName);
            actions.Add(action);
        }
        
        return action;
    }

    private GPAction FindAction(string actionName)
    {
        return actions.Find(x => x.name != null && x.name.Equals(actionName));
    }

    #endregion

    #region Goals

    private WorldState ReadGoal(string goalName)
    {
        WorldState goal = FindGoal(goalName);
        if (goal == null)
        {
            goal = new WorldState();
            goal.name = goalName;
            goals.Add(goal);
        }

        return goal;
    }
    
    private WorldState FindGoal(string goalName)
    {
        return goals.Find(x => x.name.Equals(goalName));
    }

    #endregion

    private List<GPAction> GetTransitions(WorldState worldCondition)
    {
        List<GPAction> possibleActions = new List<GPAction>();
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i].preConditions.Match(worldCondition))
            {
                possibleActions.Add(actions[i]);
            }
        }

        return possibleActions;
    }
    
    public int GetConditionIndex(string conditionName)
    {
        int index = conditions.IndexOf(conditionName);
        if (index == -1)
        {
            conditions.Add(conditionName);
            index = conditions.Count - 1;
        }

        return index;
    }

    private int FindEqual(List<GPNode> list, WorldState condition)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].worldConditions.Equal(condition))
            {
                return i;
            }
        }

        return -1;
    }

    public string GetState(string actionName)
    {
        GPAction action = FindAction(actionName);
        return action.name;
    }
}
