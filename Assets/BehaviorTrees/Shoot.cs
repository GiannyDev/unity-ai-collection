using BBUnity.Actions;
using Pada1.BBCore;
using Pada1.BBCore.Tasks;
using UnityEngine;

[Action("MyActions/Shoot")]
public class Shoot : GOAction
{
    [InParam("ShootPoint")]
    public Transform shootPoint;

    [InParam("BulletPrefab")]
    public GameObject bulletPrefab;

    [InParam("Velocity")]
    public float velocity;

    [InParam("Delay")]
    public int delay;

    private float elapsed;

    private void FireBullet()
    {
        GameObject newBullet = GameObject.Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        newBullet.GetComponent<Rigidbody>().velocity = velocity * shootPoint.forward;
    }

    public override TaskStatus OnUpdate()
    {
        if (shootPoint == null)
        {
            return TaskStatus.FAILED;
        }

        if (delay > 0)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 0.5f)
            {
                FireBullet();
                elapsed = 0f;
                return TaskStatus.COMPLETED;
            }
        }

        return TaskStatus.RUNNING;
    }
}
