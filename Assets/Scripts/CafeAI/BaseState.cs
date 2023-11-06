using UnityEngine;

namespace CafeAI
{
    public class BaseState : ScriptableObject
    {
        public virtual void Execute(BaseStateMachine machine)
        {
            // We can access the world and scene using the Unity Engine and machine.
        }
    }
}