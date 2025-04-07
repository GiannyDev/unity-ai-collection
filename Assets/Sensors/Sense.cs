using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Sense : MonoBehaviour
{
    protected bool playerDetected;
    
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateSense();
    }
    
    protected virtual void Initialize(){}
    protected virtual void UpdateSense(){}
}
