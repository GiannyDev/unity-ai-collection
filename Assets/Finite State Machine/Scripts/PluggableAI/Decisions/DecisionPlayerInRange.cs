using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Decision/PlayerInRange")]
public class DecisionPlayerInRange : AIDecision
{
    public override bool Decide(AIController controller)
    {
        return IsPlayerInRange(controller);
    }

    private bool IsPlayerInRange(AIController controller)
    {
        return controller.PlayerInRangeToChase();
    }
}
