using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BoidUnit : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float maxSteeringForce = 15f;

    private Rigidbody myRigidbody;
    private Vector3 targetPos;
    private Quaternion targetRot;
    private BoidRadar boidRadar;
    
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        boidRadar = GetComponent<BoidRadar>();
    }

    private void FixedUpdate()    
    {
        Vector3 currenPos = transform.position;
        targetPos = currenPos + myRigidbody.velocity.normalized * maxSpeed;
        targetRot = Quaternion.LookRotation(targetPos - currenPos);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 0.08f);
        myRigidbody.AddForce(Seek(targetPos));
        myRigidbody.AddForce(AvoidWalls() * WorldSettings.Instance.avoidWallsScale);
        myRigidbody.AddForce(AvoidNeighbors() * WorldSettings.Instance.avoidNeighborsScale);
        myRigidbody.AddForce(AlignWithNeighbors() * WorldSettings.Instance.alignWithNeighborsScale);
        myRigidbody.AddForce(ApplyCohesion() * WorldSettings.Instance.cohesionScale);
    }

    private Vector3 Seek(Vector3 target)
    {
        Vector3 desiredVelocity = target - transform.position;
        Vector3 computedVelocity = desiredVelocity.normalized * maxSpeed;
        return Vector3.ClampMagnitude(computedVelocity - myRigidbody.velocity, maxSteeringForce);
    }
    
    private Vector3 AvoidWalls()
    {
        bool isOutsideBounds = false;
        float offset = 1f;
        Vector3 currentPosition = transform.position;
        Vector3 desiredPosition = transform.position;

        // X
        if (currentPosition.x < WorldSettings.Instance.GetMinPosition(Dimension.X))
        {
            desiredPosition.x = WorldSettings.Instance.GetMinPosition(Dimension.X) + offset;
            isOutsideBounds = true;
        } 
        else if (currentPosition.x > WorldSettings.Instance.GetMaxPosition(Dimension.X))
        {
            desiredPosition.x = WorldSettings.Instance.GetMaxPosition(Dimension.X) - offset;
            isOutsideBounds = true;
        }
        
        // Y
        if (currentPosition.y < WorldSettings.Instance.GetMinPosition(Dimension.Y))
        {
            desiredPosition.y = WorldSettings.Instance.GetMinPosition(Dimension.Y) + offset;
            isOutsideBounds = true;
        } 
        else if (currentPosition.y > WorldSettings.Instance.GetMaxPosition(Dimension.Y))
        {
            desiredPosition.y = WorldSettings.Instance.GetMaxPosition(Dimension.Y) - offset;
            isOutsideBounds = true;
        }
        
        // Z
        if (currentPosition.z < WorldSettings.Instance.GetMinPosition(Dimension.Z))
        {
            desiredPosition.z = WorldSettings.Instance.GetMinPosition(Dimension.Z) + offset;
            isOutsideBounds = true;
        } 
        else if (currentPosition.z > WorldSettings.Instance.GetMaxPosition(Dimension.Z))
        {
            desiredPosition.z = WorldSettings.Instance.GetMaxPosition(Dimension.Z) - offset;
            isOutsideBounds = true;
        }

        if (isOutsideBounds)
        {
            Debug.DrawLine(transform.position, desiredPosition, Color.red);
            return Seek(desiredPosition);
        }

        return Vector3.zero;
    }

    private Vector3 AvoidNeighbors()
    {
        if (boidRadar.Neighbors == null)
        {
            return Vector3.zero;
        }
        
        List<Transform> neighbors = boidRadar.Neighbors;
        Vector3 separation = Vector3.zero;
        
        foreach (Transform neighbor in neighbors)
        {
            Vector3 dir = transform.position - neighbor.position;
            float separationScale = dir.sqrMagnitude;
            separation += dir.normalized / separationScale;
        }

        if (neighbors.Count > 0)
        {
            separation /= neighbors.Count;
        }

        Vector3 targetVelocity = transform.position + separation * maxSpeed;
        return Seek(targetVelocity);
    }

    private Vector3 AlignWithNeighbors()
    {
        if (boidRadar.Neighbors == null)
        {
            return Vector3.zero;
        }
        
        List<Transform> neighbors = boidRadar.Neighbors;
        Vector3 avgVelocity = Vector3.zero;

        foreach (Transform neighbor in neighbors)
        {
            avgVelocity += neighbor.GetComponent<Rigidbody>().velocity;
        }

        int count = neighbors.Count;
        if (count > 0)
        {
            avgVelocity /= count;
        }

        Vector3 targetPos = transform.position + avgVelocity;
        Debug.DrawLine(transform.position, targetPos, Color.green);
        return Seek(targetPos);
    }

    private Vector3 ApplyCohesion()
    {
        if (boidRadar.Neighbors == null)
        {
            return Vector3.zero;
        }

        List<Transform> neighbors = boidRadar.Neighbors;
        Vector3 center = Vector3.zero;

        foreach (Transform neighbor in neighbors)
        {
            center += neighbor.position;
        }

        int count = neighbors.Count;
        if (count > 0)
        {
            center /= count;
        }

        Vector3 targetPos = center;
        Debug.DrawLine(transform.position, targetPos, Color.cyan);
        return Seek(targetPos);
    }
}