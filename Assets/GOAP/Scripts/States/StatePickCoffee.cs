using UnityEngine;
using UnityEngine.UI;

public class StatePickCoffee :  GPState
{
    private float timer;
    private GPAgent owner;
    
    public StatePickCoffee(GPAgent agent) : base("PickCoffee")
    {
        owner = agent;
    }

    public override void Initialize()
    {
        owner.Blackboard.SelectedCoffee = LevelManager.Instance.PickCoffe(owner);
    }

    public override void UpdateState()
    {
        if (LevelManager.Instance.IsAgentNext(owner.Blackboard.SelectedCoffee, owner))
        {
            owner.Blackboard.PickedCoffee = true;
            isCompleted = true;
        }
    }
}