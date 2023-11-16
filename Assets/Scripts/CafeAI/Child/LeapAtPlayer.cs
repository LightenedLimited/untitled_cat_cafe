using System.Collections;
using System.Collections.Generic;
using CafeAI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class LeapAtPlayer : State
    {
        [SerializeField] State FinishedTransition;
        [SerializeField] State SuccessTransition;
        [SerializeField] float LeapDelay = 0.6f;
        [SerializeField] float LeapForce = 2f;
        private NavMeshAgent agent;
        private GameObject player;
        private float LeapCD;
        bool hasLeapt;
        private Rigidbody body;
        protected override void Start()
        {
            base.Start();
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");

            if (!TryGetComponent<Rigidbody>(out body))
                Debug.LogError("No Rigidbody");

            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }

        void OnEnable()
        {
            gameObject.transform.LookAt(player.transform);
            hasLeapt = false;
            LeapCD = LeapDelay;

            // agent.isStopped = true;
            body.isKinematic = false;
            agent.enabled = false;
        }
        void OnDisable()
        {
            body.isKinematic = true;
            agent.enabled = true;
            // agent.isStopped = false;
        }
        // Update is called once per frame
        void Update()
        {
            LeapCD -= Time.deltaTime;
            if (LeapCD <= 0)
            {
                if (!hasLeapt)
                {
                    hasLeapt = true;
                    body.AddRelativeForce(Vector3.forward * LeapForce, ForceMode.VelocityChange);
                    body.AddRelativeForce(Vector3.up * (LeapForce * 0.2f), ForceMode.VelocityChange);
                }
                else if (LeapCD < -0.5 && body.velocity.magnitude <= 0.01) // a little jank
                {
                    Debug.Log("Finished Leap");
                    manager.Transition(this, FinishedTransition);
                }
            }
        }

        void OnCollisionEnter(Collision collision)            
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Caught Player");
                MeshRenderer r;
                if(TryGetComponent(out r))
                    r.material.SetColor("_Color", Color.green);
                manager.Transition(this, SuccessTransition);
            }
        }
    }

}