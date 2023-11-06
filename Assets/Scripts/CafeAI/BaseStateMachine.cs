using UnityEngine;

// referenced from:
// https://www.toptal.com/unity-unity3d/unity-ai-development-finite-state-machine-tutorial

namespace CafeAI
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState _initialState;

        private void Awake()
        {
            CurrentState = _initialState;
        }

        public BaseState CurrentState { get; set; }

        private void Update()
        {
            CurrentState.Execute(this);
        }
    }

}