using System;
using System.Collections.Generic;
using UnityEngine;

namespace CafeAI
{
    public class BaseState : ScriptableObject
    {
        public virtual void Execute(BaseStateMachine machine) { }
    }

    [CreateAssetMenu(menuName="FSM/State")]
    public sealed class State : BaseState
    {
        public List<FSMAction> Action = new List<FSMAction>();
        public List<Transition> Transitions = new List<Transition>();
        public override void Execute(BaseStateMachine machine)
        {
            foreach (var action in Action)
                action.Execute(machine);

            // Each transition contains a Decision (whether or not to transition to a different state)
            foreach (var transition in Transitions)
                transition.Execute(machine);
            // We can access the world and scene using the Unity Engine and machine.
        }
    }

    // Dummy State for staying in a particular state
    [CreateAssetMenu(menuName = "FSM/Remain In State", fileName = "RemainInState")]
    public sealed class RemainInState : BaseState { }

    public abstract class FSMAction : ScriptableObject
    {
        public abstract void Execute(BaseStateMachine stateMachine);
    }

    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(BaseStateMachine stateMachine);
    }

    [CreateAssetMenu(menuName = "FSM/Transition")]
    public sealed class Transition : ScriptableObject
    {
        public Decision Decision;
        public BaseState TrueState;
        public BaseState FalseState;

        public void Execute(BaseStateMachine stateMachine)
        {
            if(Decision.Decide(stateMachine) && !(TrueState is RemainInState))
                stateMachine.CurrentState = TrueState;
            else if(!(FalseState is RemainInState))
                stateMachine.CurrentState = FalseState;
        }
    }
}