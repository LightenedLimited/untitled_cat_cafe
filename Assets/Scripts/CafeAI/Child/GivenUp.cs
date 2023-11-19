using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class GivenUp : State
    {
        private Rigidbody body;
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<Rigidbody>(out body))
                Debug.LogError("No Rigidbody");
        }

        void OnEnable()
        {
            NavMeshAgent agent;
            if (TryGetComponent(out agent))
                agent.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            body.AddTorque(transform.up);
            body.AddTorque(transform.right);
        }
    }
}