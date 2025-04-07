using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Touch : Sense
{
    private Transform player;
    
    protected override void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDetectable detectable = other.GetComponent<IDetectable>();
        if (detectable != null)
        {
            playerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDetectable detectable = other.GetComponent<IDetectable>();
        if (detectable != null)
        {
            playerDetected = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (player == null)
        {
            return;
        }
        
        Debug.DrawLine(transform.position, player.position, playerDetected ? Color.green : Color.red);
    }
}
