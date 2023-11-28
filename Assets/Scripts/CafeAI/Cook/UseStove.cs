using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class UseStove : State 
    {
        [SerializeField] public float InteractionReach = 2f;
        [SerializeField] public State FinishedTransition;
        [SerializeField] public State FireTransition;
        [SerializeField] public float TransitionInterval = 5f;
        [SerializeField] public float TransitionProbability = 0.1f;
        private float timer = 0f;

        // Start is called before the first frame update
        private NavMeshAgent agent;
        private IPatience patienceMan;
        private CookStateMan cookMan;
        private PlayerController player;
        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent(out agent))
                Debug.LogError("No Navmesh Agent");
            if (!TryGetComponent(out player))
                Debug.Log("No Player");
            if (!TryGetComponent(out cookMan))
                Debug.Log("StateManager is not a Cook");
            if (manager is IPatience)
                patienceMan = manager as IPatience;
        }
        void OnEnable()
        {
            if (Vector3.Distance(transform.position, cookMan.Stove.transform.position) >= InteractionReach) 
                agent.destination = cookMan.Stove.transform.position;
            timer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            // we've arrived
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (timer <= 0)
                {
                    cookMan.Stove.Use();
                    if (cookMan.Stove.OnFire)
                        manager.Transition(this, FireTransition);
                }
                // go to the fridge now and then
                if (timer >= TransitionInterval)
                {
                    timer -= TransitionInterval;
                    if (Random.value >= TransitionProbability)
                    {
                        manager.Transition(this, FinishedTransition);
                    }
                }

                timer += Time.deltaTime;
            }
        }
    }
}