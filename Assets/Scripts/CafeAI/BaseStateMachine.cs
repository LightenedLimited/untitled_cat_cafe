using System;
using System.Collections.Generic;
using UnityEngine;

// referenced from:
// https://www.toptal.com/unity-unity3d/unity-ai-development-finite-state-machine-tutorial
// TODO: we probably want additional state to be stored inside some monobehavior or data structure
// We can probably do this by writing a small data container behavior for each NPC type?

namespace CafeAI
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState _initialState;
        private Dictionary<Type, Component> _cachedComponents;

        private void Awake()
        {
            CurrentState = _initialState;
            _cachedComponents = new Dictionary<Type, Component>();
        }

        public BaseState CurrentState { get; set; }

        private void Update() {
            CurrentState.Execute(this);
        }

        // Overrides default GetComponent to cache
        // Notes for Derek - new: overrides base GetComponent
        // Where T: Component: restricts which classes can be passed.
        public new T GetComponent<T>() where T : Component
        {
            if (_cachedComponents.ContainsKey(typeof(T)))
                return _cachedComponents[typeof(T)] as T;
            
            var component = base.GetComponent<T>();
            if (component != null)
            {
                _cachedComponents.Add(typeof(T), component);
            }
            return component;
        }
    }

}