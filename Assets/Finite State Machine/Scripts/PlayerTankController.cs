using UnityEngine;

public class PlayerTankController : MonoBehaviour, ITank
{
    [SerializeField] private GameObject tankTurret;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private int health = 10;

    public int CurrentHealth { get; set; }
    
    private float currentSpeed;
    private float targetSpeed;
    private float rotationSpeed = 150f;

    private float maxForwardSpeed = 15f;
    private float maxBackwardSpeed = -10f;

    private Transform _bulletSpawnPosition;
    private Transform _turret;
    private Camera _camera;

    private float shoorRate = 3f;
    private float elapsedTime;
    
    private void Start()
    {
        _turret = tankTurret.transform;
        _bulletSpawnPosition = bulletSpawnPos;
        CurrentHealth = health;
        _camera = Camera.main;
    }
    
    private void Update()
    {
        MoveTank();
        RotateTurret();
        FireBullet();
    }

    private void MoveTank()
    {
        if (Input.GetKey(KeyCode.W))
        {
            targetSpeed = maxForwardSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            targetSpeed = maxBackwardSpeed;
        }
        else
        {
            targetSpeed = 0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 10f);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void RotateTurret()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (playerPlane.Raycast(ray, out float hit))
        {
            Vector3 hitPos = ray.GetPoint(hit);
            Quaternion newRotation = Quaternion.LookRotation(hitPos - transform.position);
            _turret.rotation = Quaternion.Slerp(_turret.rotation, newRotation, Time.deltaTime * 10f);
        }
    }

    private void FireBullet()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= shoorRate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bulletPrefab, _bulletSpawnPosition.position, _bulletSpawnPosition.rotation);
                elapsedTime = 0f;
            }
        }
    }

    public void ReceiveDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
