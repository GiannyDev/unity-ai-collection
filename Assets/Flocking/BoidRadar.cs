using System;
using System.Collections.Generic;
using UnityEngine;

public class BoidRadar : MonoBehaviour
{
    public List<Transform> Neighbors => neighbors;
    private List<Transform> neighbors;
    private int skipTimer = 10;
    
    private void Update()
    {
        if (Time.frameCount % skipTimer == 0)
        {
            neighbors = new List<Transform>();
            int boidLayer = 1 << LayerMask.NameToLayer("Boid");
            Collider[] surroundingBoids = Physics.OverlapSphere(transform.position,
                WorldSettings.Instance.neighborRadarRadius, boidLayer);
            foreach (Collider boid in surroundingBoids)
            {
                if (boid.gameObject != gameObject)
                {
                    neighbors.Add(boid.transform);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, WorldSettings.Instance.neighborRadarRadius);
        foreach (Transform neighbor in neighbors)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, neighbor.position);
        }
    }
}