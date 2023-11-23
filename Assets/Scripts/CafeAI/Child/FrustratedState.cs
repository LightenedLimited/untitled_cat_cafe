using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class FrustratedState : State 
    {
        // Start is called before the first frame update
        [SerializeField] State FinishedState;
        [SerializeField] State OutOfPatienceState;
        [SerializeField] float FrustrationLength = 5f; 
        private UnityEngine.AI.NavMeshAgent agent;
        private IPatience patienceMan;
        private float frustrationTime;
        private Rigidbody body;
        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
            if (!TryGetComponent<Rigidbody>(out body))
                Debug.LogError("No Rigidbody");
            if (manager is not IPatience)
                Debug.LogError("Statemanager needs to have patience");
            else
                patienceMan = (IPatience)manager;
        }

        void OnEnable()
        {
            agent.enabled = false;
            body.isKinematic = false;
            frustrationTime = FrustrationLength;
            patienceMan.Patience--;
        }
        // Update is called once per frame
        void Update()
        {
            body.AddTorque(transform.up, ForceMode.VelocityChange);
            frustrationTime -= Time.deltaTime;
            if (frustrationTime <= 0)
            {
                if (patienceMan.Patience <= 0)
                    manager.Transition(this, OutOfPatienceState);
                else
                    manager.Transition(this, FinishedState);
            }
        }
        void OnDisable()
        {
            agent.enabled = true;
            body.isKinematic = true;
        }
    }

}