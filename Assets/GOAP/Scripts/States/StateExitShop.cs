using UnityEngine;
using UnityEngine.AI;

public class StateExitShop : GPState
{
    private GPAgent owner;
    private NavMeshAgent navMesh;
    private Vector3 exitPos;
    
    public StateExitShop(GPAgent agent) : base("ExitShop")
    {
        owner = agent;
    }

    public override void Initialize()
    {
        navMesh = owner.GetComponent<NavMeshAgent>();
        exitPos = owner.Blackboard.ShopExitPosition;
    }

    public override void UpdateState()
    {
        navMesh.SetDestination(exitPos);
        if (Vector3.Distance(navMesh.transform.position, exitPos) <= 1f)
        {
            owner.Blackboard.GotCoffee = true;
            isCompleted = true;
        }
    }
}