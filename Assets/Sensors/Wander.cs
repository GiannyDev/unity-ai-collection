using UnityEngine;

public class Wander : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;

    private Vector3 targetPos;
    private float minX, maxX;
    private float minZ, maxZ;

    private void Start()
    {
        minX = -15f;
        maxX = 15f;
        minZ = -15f;
        maxZ = 15f;
        
        GetNextRandomPos();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) < 2f)
        {
            GetNextRandomPos();
        }
        
        Quaternion newRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void GetNextRandomPos()
    {
        targetPos = new Vector3(Random.Range(minX, maxX), 1.5f, Random.Range(minZ, maxZ));
    }
}
