using UnityEngine;

public class GPAgent : MonoBehaviour
{
    [SerializeField] private CurrentWorld currentWorldInfo;
    public GPAgentData AgentData { get; set; }
    public AgentBlackboard Blackboard { get; set; }
    
    private void Awake()
    {
        Blackboard = GetComponent<AgentBlackboard>();
        
        AgentData = new GPAgentData(gameObject);
        AgentData.planner.LoadCurrentWorldInfo(currentWorldInfo);

        AgentData.states = new GPState[]
        {
            new StateIdle(this),
            new StateEnterShop(this),
            new StatePickCoffee(this),
            new StateMoveToCounter(this),
            new StateOrderCoffee(this),
            new StateExitShop(this)
        };
        
        AgentData.SetDefaultState("Idle");
        AgentData.SetGoal("BuyCoffee");
    }

    private void Update()
    {
        AgentData.ExecuteBehavior();
        AgentData.UpdateCurrentState();
    }
}
