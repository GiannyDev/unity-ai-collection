using UnityEngine;

public class FSM : MonoBehaviour
{
    protected Transform playerTransform;
    protected Vector3 destinationPos;
    protected GameObject[] wandarPoints;
    
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        FSMUpdate();
    }

    protected virtual void Initialize()
    {
        
    }

    protected virtual void FSMUpdate()
    {
        
    }
}
