using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public abstract class State : MonoBehaviour
    {
        protected StateMan manager;
        protected virtual void Awake()
        {
            if (!gameObject.TryGetComponent(out manager))
                Debug.Log("GameObject doesn't have a State Manager");
        }
    }

    // State managers will also need to hold information that persists between states
    public abstract class StateMan : MonoBehaviour
    {
        [SerializeField] public State StartingState;
        public State ActiveState;
        protected virtual void Awake()
        {
            var states = gameObject.GetComponents<State>();
            foreach (State s in states)
            {
                // Debug.Log(s == StartingState);
                s.enabled = (s == StartingState);
            }
            ActiveState = StartingState;
        }
        
        public void Transition(State from, State to)
        {
            if (from == to)
            {
                Debug.Log("transitioning to self");
                return;
            }
            from.enabled = false;
            if (to is not null)
                to.enabled = true;
            ActiveState = to;
        }
    }
}