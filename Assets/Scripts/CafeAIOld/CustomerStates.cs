using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CafeAI
{
    [CreateAssetMenu(menuName = "FSM/Actions/GoToCoffee")]
    public class GoToCoffeeAction : FSMAction
    {
        public override void Execute (BaseStateMachine stateMachine)
        {
            Debug.Log("Going to Coffee");
            NavMeshAgent navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(new Vector3(10, 0, 10));
        }
    }

    [CreateAssetMenu(menuName = "FSM/Actions/InteractWithCoffee")]
    public class InteractWithCoffeeAction : FSMAction
    {
        public override void Execute (BaseStateMachine stateMachine)
        {
            Debug.Log("Interacting with Coffee");
        }
    }

    [CreateAssetMenu(menuName = "FSM/Decisions/FindToInteract")]
    public class FindToInteractDecision : Decision
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            return true;
        }
    }

    [CreateAssetMenu(menuName = "FSM/Decisions/InteractToFind")]
    public class InteractToFindDecision : Decision 
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            return true;
        }
    }
}