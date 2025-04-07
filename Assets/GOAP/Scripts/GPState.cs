using UnityEngine;

public class GPState
{
    public string name;
    protected bool isCompleted;
    public bool IsCompleted => isCompleted;

    public GPState(string pName)
    {
        name = pName;
        isCompleted = false; // -------------------
    }

    public virtual void Initialize()
    {
        
    }

    public virtual void UpdateState()
    {
        
    }

    public virtual bool IsValid()
    {
        return true;
    }

    public virtual void Perform()
    {
        
    }
}
