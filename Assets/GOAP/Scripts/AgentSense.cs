using UnityEngine;

public class AgentSense
{
    private AgentBlackboard blackboard;
    public AgentSense(GameObject agent)
    {
        blackboard = agent.GetComponent<AgentBlackboard>();
    }

    public void GetConditions(GPAgentData agentData, WorldState worldState)
    {
        worldState.SetCondition(agentData.planner, "InsideShop", blackboard.InsideShop);
        worldState.SetCondition(agentData.planner, "InCounter", blackboard.InCounter);
        worldState.SetCondition(agentData.planner, "PickedCoffee", blackboard.PickedCoffee);
        worldState.SetCondition(agentData.planner, "GotCoffee", blackboard.GotCoffee);
        worldState.SetCondition(agentData.planner, "PurchasedCoffee", blackboard.PurchasedCoffee);
        worldState.SetCondition(agentData.planner, "ShopOpen", true);
    }
}