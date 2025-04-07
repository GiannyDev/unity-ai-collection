using UnityEditor.Timeline;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTankAI : FSM, ITank
{
    public enum FSMStates
    {
        Patrol,
        Chase,
        Attack,
        Dead
    }

    [SerializeField] private FSMStates currentState = FSMStates.Patrol;
    [SerializeField] private GameObject enemyTankTurret;
    [SerializeField] private Transform enemyTankBulletSpawnPos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int health = 10;

    public int CurrentHealth { get; set; }
    
    private Transform turret;
    private Transform bulletSpawnPos;
    private float shootRate = 3f;
    private float elapsedTime;
    private bool isDead;
    
    protected override void Initialize()
    {
        wandarPoints = GameObject.FindGameObjectsWithTag("WandarPoint");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        turret = enemyTankTurret.transform;
        bulletSpawnPos = enemyTankBulletSpawnPos;
        CurrentHealth = health;
        
        FindNextDestination();
    }

    protected override void FSMUpdate()
    {
        switch (currentState)
        {
            case FSMStates.Patrol : StatePatrol(); break;
            case FSMStates.Chase : StateChase(); break;
            case FSMStates.Attack : StateAttack(); break;
            case FSMStates.Dead : StateDead(); break;
        }

        elapsedTime += Time.deltaTime;
        if (CurrentHealth <= 0)
        {
            currentState = FSMStates.Dead;
        }
    }

    private void StatePatrol()
    {
        if (Vector3.Distance(transform.position, destinationPos) <= 5f)
        {
            FindNextDestination();
        }
        else if (Vector3.Distance(transform.position, playerTransform.position) <= 20f)
        {
            currentState = FSMStates.Chase;
        }

        MoveAndRotateTowardsDestination();
    }

    private void StateChase()
    {
        destinationPos = playerTransform.position;

        float distanceToAttack = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToAttack <= 10f)
        {
            currentState = FSMStates.Attack;
        }
        else if (distanceToAttack >= 20f)
        {
            currentState = FSMStates.Patrol;
        }
        
        MoveAndRotateTowardsDestination();
    }

    private void StateAttack()
    {
        destinationPos = playerTransform.position;
        float distanceToAttack = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToAttack < 20f)
        {
            MoveAndRotateTowardsDestination();
            currentState = FSMStates.Attack;
        }
        else if (distanceToAttack >= 20f)
        {
            currentState = FSMStates.Patrol;
        }
        
        Quaternion newRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        turret.rotation = Quaternion.Slerp(turret.rotation, newRotation, Time.deltaTime * 10f);
        
        ShootBullet();
    }

    private void StateDead()
    {
        if (!isDead)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    private void FindNextDestination()
    {
        int randomIndex = Random.Range(0, wandarPoints.Length);
        destinationPos = wandarPoints[randomIndex].transform.position;
    }
    private void MoveAndRotateTowardsDestination()
    {
        Quaternion targetRotation = Quaternion.LookRotation(destinationPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        transform.Translate(Vector3.forward * 6f * Time.deltaTime);
    }

    private void ShootBullet()
    {
        if (elapsedTime >= shootRate)
        {
            Instantiate(bulletPrefab, bulletSpawnPos.position, bulletSpawnPos.rotation);
            elapsedTime = 0f;
        }
    }

    public void ReceiveDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}
