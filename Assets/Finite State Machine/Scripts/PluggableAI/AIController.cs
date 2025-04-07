using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    [SerializeField] private AIState currentState;
    [SerializeField] private AIState remainState;

    [Header("Path")] 
    [SerializeField] private GameObject[] wandarPoints;

    [Header("Player")]
    [SerializeField] private Transform playerTank;

    [SerializeField] private Transform turret;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletSpawnPos;
    
    private Vector3 nextDestination;
    private float shootRate = 2f;
    private float minDistanceToAttack = 10f;
    private float elapsedTime;

    private void Start()
    {
        FindNextDestination();
    }

    private void Update()
    {
        currentState.RunState(this);
        elapsedTime += Time.deltaTime;
    }

    public void ChangeState(AIState newState)
    {
        if (newState != remainState)
        {
            currentState = newState;
        }
    }


    #region Actions

    public void FindNextDestination()
    {
        int randomIndex = Random.Range(0, wandarPoints.Length);
        nextDestination = wandarPoints[randomIndex].transform.position;
    }

    public void MoveAndRotateTowardsDestination()
    {
        Quaternion targetRotation = Quaternion.LookRotation(nextDestination - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        transform.Translate(Vector3.forward * 6f * Time.deltaTime);
    }
    
    public void UpdateNextDestinationTowardsPlayer()
    {
        if (playerTank != null)
        {
            nextDestination = playerTank.position;
        }
    }

    public void RotateTurretTowardsTarget()
    {
        if (playerTank == null)
        {
            return;
        }
        
        Quaternion newRotation = Quaternion.LookRotation(playerTank.position - transform.position);
        turret.rotation = Quaternion.Slerp(turret.rotation, newRotation, Time.deltaTime * 10f);
    }
    
    public void ShootPlayerTank()
    {
        if (elapsedTime >= shootRate)
        {
            Instantiate(bulletPrefab, bulletSpawnPos.transform.position, bulletSpawnPos.transform.rotation);
            elapsedTime = 0f;
        }
    }
    
    #endregion

    #region Decisions

    public bool CloseToDestination()
    {
        float distance = Vector3.Distance(transform.position, nextDestination);
        if (distance <= 5f)
        {
            return true;
        }

        return false;
    }

    public bool PlayerInRangeToChase()
    {
        if (playerTank == null)
        {
            return false;
        }
        
        float distanceToPlayer = Vector3.Distance(playerTank.position, transform.position);
        if (distanceToPlayer <= 15f)
        {
            return true;
        }

        return false;
    }

    public bool PlayerInRangeToAttack()
    {
        if (playerTank == null)
        {
            return false;
        }
        
        float distanceToPlayer = Vector3.Distance(playerTank.position, transform.position);
        if (distanceToPlayer <= minDistanceToAttack)
        {
            return true;
        }

        return false;
    }
    
    #endregion
}
