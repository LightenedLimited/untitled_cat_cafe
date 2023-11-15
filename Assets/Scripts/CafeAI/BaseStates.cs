using System;
using System.Collections;
using System.Collections.Generic;
using CafeAI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public abstract class State : MonoBehaviour
    {
        protected StateMan manager;
        public virtual void Start(){
            if (!gameObject.TryGetComponent(out manager))
            {
                Debug.Log("GameObject doesn't have a State Manager");
            }
        }
    }

    // State managers will also need to hold information that persists between states
    public abstract class StateMan : MonoBehaviour
    {
        public State activeState;
        
        public void Transition(State from, State to)
        {
            from.enabled = false;
            to.enabled = true;
            activeState = to;
        }
    }
}