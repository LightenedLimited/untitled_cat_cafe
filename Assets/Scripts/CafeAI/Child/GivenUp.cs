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
        private NavMeshAgent agent;
        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<Rigidbody>(out body))
                Debug.LogError("No Rigidbody");
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Agent");
        }

        void OnEnable()
        {
            agent.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            body.AddTorque(transform.up*0.5f, ForceMode.VelocityChange);
        }
    }
}