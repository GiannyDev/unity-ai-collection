using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoffeeType
{
    None,
    Espresso,
    Cappuccino,
    Macchiato,
    Latte
}

public class CoffeeCounter : MonoBehaviour
{
    [SerializeField] private CoffeeType coffee;
    [SerializeField] private Transform orderPosition;

    public CoffeeType Coffee => coffee;
    public Vector3 OrderPosition => orderPosition.position;

    private Queue<GPAgent> customersInLine = new Queue<GPAgent>();

    public void AddCustomer(GPAgent agent)
    {
        customersInLine.Enqueue(agent);
    }

    public void RemoveCustomer()
    {
        customersInLine.Dequeue();
    }

    public GPAgent GetNextCustomer()
    {
        return customersInLine.Peek();
    }
}
