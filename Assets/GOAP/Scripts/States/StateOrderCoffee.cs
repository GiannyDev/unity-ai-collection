using UnityEngine;

public class StateOrderCoffee : GPState
{
    private float timer;
    private GPAgent owner;
    private CoffeeCounter counter;
    
    public StateOrderCoffee(GPAgent agent) : base("OrderCoffee")
    {
        owner = agent;
    }

    public override void Initialize()
    {
        timer = Random.Range(3f, 6f);
        counter = LevelManager.Instance.GetCounter(owner.Blackboard.SelectedCoffee);
    }

    public override void UpdateState()
    {
        if (IsValid())
        {
            counter.RemoveCustomer();
            owner.Blackboard.PurchasedCoffee = true;
            isCompleted = true;
        }
    }

    public override bool IsValid()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            return true;
        }

        return false;
    }
}