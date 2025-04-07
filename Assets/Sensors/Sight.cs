using System;
using UnityEngine;

public class Sight : Sense
{
    public int fieldOfView = 45;
    public int viewDistance = 30;
    
    private Transform playerTransform;
    
    protected override void Initialize()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void UpdateSense()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        RaycastHit hitInfo;
        Vector3 rayDirection = playerTransform.position - transform.position;
        if (Vector3.Angle(rayDirection, transform.forward) < fieldOfView)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hitInfo, viewDistance))
            {
                IDetectable player = hitInfo.collider.GetComponent<IDetectable>();
                if (player != null)
                {
                    playerDetected = true;
                }
                else
                {
                    playerDetected = false;
                }
            }
        }
        else
        {
            playerDetected = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isEditor && playerTransform == null)
        {
            return;
        }

        if (playerDetected)
        {
            Debug.DrawLine(transform.position, playerTransform.position, Color.green);
        }

        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0f, -fieldOfView, 0f) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0f, fieldOfView, 0f) * frontRayPoint;
        
        Debug.DrawLine(transform.position, frontRayPoint, Color.blue);
        Debug.DrawLine(transform.position, leftRayPoint, Color.blue);
        Debug.DrawLine(transform.position, rightRayPoint, Color.blue);
    }
}
