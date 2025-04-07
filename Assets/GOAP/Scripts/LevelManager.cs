using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance => instance;
    
    [SerializeField] private Transform shopEnterPos;
    [SerializeField] private Transform shopExitPos;
    [SerializeField] private CoffeeCounter[] counters;
    
    public Vector3 ShopEnterPosition => shopEnterPos.position;
    public Vector3 ShopExitPosition => shopExitPos.position;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public CoffeeType PickCoffe(GPAgent agent)
    {
        int counterIndex = Random.Range(0, counters.Length);
        counters[counterIndex].AddCustomer(agent);
        return counters[counterIndex].Coffee;
    }

    public CoffeeCounter GetCounter(CoffeeType selectedCoffee)
    {
        foreach (CoffeeCounter counter in counters)
        {
            if (counter.Coffee == selectedCoffee)
            {
                return counter;
            }
        }

        return null;
    }

    public bool IsAgentNext(CoffeeType selectedCoffee, GPAgent agent)
    {
        CoffeeCounter counter = GetCounter(selectedCoffee);
        GPAgent nextCustomer = counter.GetNextCustomer();

        if (nextCustomer == agent)
        {
            return true;
        }

        return false;
    }

    public Vector3 GetCounterPosition(CoffeeType selectedCoffee)
    {
        Vector3 counterPos = GetCounter(selectedCoffee).OrderPosition;
        if (counterPos != Vector3.zero)
        {
            return counterPos;
        }
        
        return Vector3.zero;
    }
}
