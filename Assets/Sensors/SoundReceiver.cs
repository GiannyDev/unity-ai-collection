using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReceiver : MonoBehaviour
{
    public float soundThresholdToDetectPlayer;
    public float soundThresholdToFollowPlayer;
    private Transform emitterPos;
    
    public void Receive(float intensity, Transform emitter)
    {
        Debug.Log("Someone is close!!!");
        if (intensity > soundThresholdToFollowPlayer)
        {
            emitterPos = emitter;
        }
    }

    private void Update()
    {
        if (emitterPos == null)
        {
            return;
        }

        Vector3 dirToEmitter = emitterPos.position - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(dirToEmitter);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 5f * Time.deltaTime);
        transform.Translate(Vector3.forward * 8f * Time.deltaTime);
    }
}
