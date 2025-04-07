using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDetectable
{
    public Transform target;
    public float moveSpeed;
    public float rotSpeed;

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < 2f)
        {
            return;
        }

        Vector3 targetPos = target.position;
        targetPos.y = transform.position.y;
        Vector3 direction = targetPos - transform.position;
        
        Quaternion newRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0f, 0f, moveSpeed * Time.deltaTime));
    }
}
