using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 60f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 2;
    
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ITank>() != null)
        {
            other.GetComponent<ITank>().ReceiveDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
