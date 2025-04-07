using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smeller : MonoBehaviour
{
    private Vector3 target;
    private Dictionary<int, GameObject> particles;
    
    private void Start()
    {
        particles = new Dictionary<int, GameObject>();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, target, Color.yellow);
    }

    private void UpdateTarget()
    {
        Vector3 centroid = Vector3.zero;
        foreach (GameObject particle in particles.Values)
        {
            Vector3 pos = particle.transform.position;
            centroid += pos;
        }

        target = centroid / particles.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        OdorEmitter odorEmitter = other.GetComponent<OdorEmitter>();
        if (odorEmitter != null)
        {
            GameObject obj = other.gameObject;
            int objId = obj.GetInstanceID();
            particles.Add(objId, obj);
            UpdateTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OdorEmitter odorEmitter = other.GetComponent<OdorEmitter>();
        if (odorEmitter != null)
        {
            GameObject obj = other.gameObject;
            int objId = obj.GetInstanceID();
            bool isRemoved = particles.Remove(objId);
            if (isRemoved)
            {
                UpdateTarget();
            }
        }
    }
}
