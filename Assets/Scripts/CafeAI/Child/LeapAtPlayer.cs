using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] float LeapDuration = 0.5f;
        private NavMeshAgent agent;
        private GameObject player;
        private float LeapTimer;
        bool hasLeapt;
        private Rigidbody body;
        private IPatience patienceMan;
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
            player = GetPlayer();
        }
        private GameObject GetPlayer()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 0)
            {
                Debug.LogError("No Player (Tag an Object with 'Player')");
                return null;
            }
            else
                return players[0];
        }

        void OnEnable()
        {
            if (player is null)
                player = GetPlayer();
            if (player is not null)
                gameObject.transform.LookAt(player.transform);
            hasLeapt = false;
            LeapTimer = 0;

            body.isKinematic = false;
            agent.enabled = false;
        }
        void OnDisable()
        {
            body.isKinematic = true;
            agent.enabled = true;
        }
        void Update()
        {
            LeapTimer += Time.deltaTime;
            if (LeapTimer >= LeapDelay)
            {
                if (!hasLeapt)
                {
                    hasLeapt = true;
                    body.AddRelativeForce(Vector3.forward * LeapForce, ForceMode.VelocityChange);
                    body.AddRelativeForce(Vector3.up * (LeapForce * 0.2f), ForceMode.VelocityChange);
                }
                else if (LeapTimer >= LeapDelay+LeapDuration && body.velocity.magnitude <= 0.01) // a little jank
                {
                    // Debug.Log("Finished Leap");
                    manager.Transition(this, FinishedTransition);
                }
            }
        }

        void OnCollisionEnter(Collision collision)            
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Caught Player");

                PlayerController playerMan;
                if (player.TryGetComponent(out playerMan))
                    playerMan.Yeet();
                
                // add some recoil to the child
                body.AddForce(Vector3.Normalize(player.transform.position + transform.position)*2 + new Vector3(0,1,0), ForceMode.VelocityChange);

                patienceMan.Patience = patienceMan.MaxPatience;
                MeshRenderer r;
                if(TryGetComponent(out r))
                    r.material.SetColor("_Color", Color.green);
                manager.Transition(this, SuccessTransition);
            }
        }
    }

}