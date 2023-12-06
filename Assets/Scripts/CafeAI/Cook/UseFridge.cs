using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class UseFridge : State 
    {
        [SerializeField] public State FinishedTransition;
        [SerializeField] public float InteractionReach = 2f;
        [SerializeField] public float Duration = 3f;
        private float timer = 0f;

        // Start is called before the first frame update
        private NavMeshAgent agent;
        private IPatience patienceMan;
        private CookStateMan cookMan;
        private PlayerController player;
        private Animator anim; 

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

            anim = GetComponent<Animator>(); 
        }
        void OnEnable()
        {
            if (Vector3.Distance(transform.position, cookMan.Fridge.transform.position) >= InteractionReach) 
                agent.destination = cookMan.Fridge.transform.position;
            timer = 0;
            anim.SetTrigger("open_fridge"); 
        }

        // Update is called once per frame
        void Update()
        {
            // we've arrived
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (timer == 0)
                {
                    cookMan.Fridge.Open();
                    // TODO: maybe take something out of the fridge
                }
                timer += Time.deltaTime;
                if (timer >= Duration)
                    manager.Transition(this, FinishedTransition);
            }
        }
    }
}