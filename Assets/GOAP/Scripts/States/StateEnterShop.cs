using UnityEngine;
using UnityEngine.AI;

public class StateEnterShop : GPState
{
    private GPAgent owner;
    private NavMeshAgent navMesh;
    private Vector3 shopEnterPos;
    
    public StateEnterShop(GPAgent agent) : base("EnterShop")
    {
        owner = agent;
    }

    public override void Initialize()
    {
        navMesh = owner.GetComponent<NavMeshAgent>();
        shopEnterPos = owner.Blackboard.ShopEnterPosition;
    }

    public override void UpdateState()
    {
        if (shopEnterPos == Vector3.zero)
        {
            return;
        }
        
        Perform();
    }

    public override void Perform()
    {
        navMesh.SetDestination(shopEnterPos);
        if (Vector3.Distance(navMesh.transform.position, shopEnterPos) <= 3f)
        {
            owner.Blackboard.InsideShop = true;
            isCompleted = true;
        }
    }
}
