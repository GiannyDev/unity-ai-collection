using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Decision/Player In Range To Attack")]
public class DecisionPlayerInRangeToAttack : AIDecision
{
    public override bool Decide(AIController controller)
    {
        return controller.PlayerInRangeToAttack();
    }
}
