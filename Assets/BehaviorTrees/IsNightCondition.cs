using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;

[Condition("MyConditions/IsNight")]
public class IsNightCondition : ConditionBase
{
    private DayNightCycle _dayNightCycle;

    private bool LightExist()
    {
        if (_dayNightCycle != null)
        {
            return true;
        }

        GameObject lightComp = GameObject.FindGameObjectWithTag("MainLight");
        if (lightComp == null)
        {
            return false;
        }

        _dayNightCycle = lightComp.GetComponent<DayNightCycle>();
        return _dayNightCycle != null;
    }
    
    public override bool Check()
    {
        if (LightExist())
        {
            return _dayNightCycle.IsNight;
        }

        return false;
    }
}