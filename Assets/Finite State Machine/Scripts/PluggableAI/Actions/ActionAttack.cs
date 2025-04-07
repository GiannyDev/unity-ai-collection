using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Attack Player")]
public class ActionAttack : AIAction
{
    public override void Act(AIController controller)
    {
        AttackPlayer(controller);
    }

    private void AttackPlayer(AIController controller)
    {
        controller.RotateTurretTowardsTarget();
        controller.MoveAndRotateTowardsDestination();
        controller.ShootPlayerTank();
    }
}
