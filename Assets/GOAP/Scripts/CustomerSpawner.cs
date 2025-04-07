using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private GPAgent customerPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private int customerAmount;

    private void Start()
    {
        SpawnCustomer();
    }

    private void SpawnCustomer()
    {
        for (int i = 0; i < customerAmount; i++)
        {
            GPAgent newCustomer = Instantiate(customerPrefab, spawnPosition.position, Quaternion.identity);
            newCustomer.Blackboard.ShopEnterPosition = LevelManager.Instance.ShopEnterPosition;
            newCustomer.Blackboard.ShopExitPosition = LevelManager.Instance.ShopExitPosition;

            float randomSpeed = Random.Range(2f, 6f);
            newCustomer.GetComponent<NavMeshAgent>().speed = randomSpeed;
        }
    }
}