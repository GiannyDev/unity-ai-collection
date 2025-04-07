using UnityEngine;

public class StateIdle : GPState
{
    private float timer;
    
    public StateIdle(GPAgent agent) : base("Idle")
    {
        
    }

    public override void Initialize()
    {
        timer = Random.Range(1f, 3f);
    }

    public override void UpdateState()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            isCompleted = true;
        }
    }
}