using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

// A generic implementation of an idle state which supports transitioning to other states
// and per-frame function execution
namespace CatCafeAI
{
    public class Idle : State
    {

        [System.Serializable]
        public struct Transition
        {
            public float probability;
            public State next;
        }
        [SerializeField] public List<Transition> Transitions;
        [SerializeField] public float ActionInterval = 2f;

        private float actionTimer = 0;
        private float probSum;
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            probSum = Transitions.Aggregate(0f, (sum, tr) => sum += tr.probability);
        }
        void OnEnable()
        {
            actionTimer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            actionTimer += Time.deltaTime;
            if (actionTimer >= ActionInterval)
            {
                actionTimer -= ActionInterval;
                // choose a transition
                float nextVal = UnityEngine.Random.value;
                for (int i = 0; i < Transitions.Count; i++)
                {
                    nextVal -= Transitions[i].probability/probSum;
                    if (nextVal <= 0)
                    {
                        manager.Transition(this, Transitions[i].next);
                        break;
                    }
                }
            }
        }
    }
}