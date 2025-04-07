using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action/Chase Player")]
public class ActionChase : AIAction
{
    public override void Act(AIController controller)
    {
        ChasePlayer(controller);
    }

    private void ChasePlayer(AIController controller)
    {
        controller.UpdateNextDestinationTowardsPlayer();
        controller.MoveAndRotateTowardsDestination();
    }
}
