using UnityEngine;
using UnityEngine.AI;

public class StateMoveToCounter : GPState
{
    private GPAgent owner;
    private NavMeshAgent navMesh;
    private Vector3 counterPos;
    
    public StateMoveToCounter(GPAgent agent) : base("MoveToCounter")
    {
        owner = agent;
    }

    public override void Initialize()
    {
        navMesh = owner.GetComponent<NavMeshAgent>();
        counterPos = LevelManager.Instance.GetCounterPosition(owner.Blackboard.SelectedCoffee);
    }

    public override void UpdateState()
    {
        if (IsValid())
        {
            Perform();
        }
    }

    public override bool IsValid()
    {
        if (owner.Blackboard.SelectedCoffee != CoffeeType.None)
        {
            return true;
        }

        return false;
    }

    public override void Perform()
    {
        navMesh.SetDestination(counterPos);
        if (Vector3.Distance(navMesh.transform.position, counterPos) <= 1f)
        {
            owner.Blackboard.InCounter = true;
            isCompleted = true;
        }
    }
}