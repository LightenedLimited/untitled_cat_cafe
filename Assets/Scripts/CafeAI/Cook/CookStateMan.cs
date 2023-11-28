using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    // The Cook cooks and checks the fridge. 
    // If he is interrupted while checking the fridge, he will lose patience
    // If he runs out of patience, he will get mad and leave
    public class CookStateMan : StateMan, IPatience
    {
        // TODO: We might need some mechanism in stateman which lets me run code every frame and 
        // potentially make transitions
        // Start is called before the first frame update
        [SerializeField] public Fridge Fridge;
        [SerializeField] public Stove Stove;
        [SerializeField] State PanicState;
        [SerializeField] private int patience = 3;
        [SerializeField] private int maxPatience = 3;
        public int Patience 
        {
            get => patience;
            set => patience = value;
        }
        public int MaxPatience => maxPatience;
        private NavMeshAgent agent;
        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
        }
        public void Panic()
        {
            Debug.Log("Chef is panicking (and so is this developer)");
            Transition(ActiveState, PanicState);
        }

        public void Interrupt()
        {
            if (ActiveState is UseFridge && agent.velocity.magnitude < 0.15f)
                Panic();
            else if (ActiveState is UseStove && agent.velocity.magnitude < 0.15f)
                Panic();
        }
        void Update ()
        {
        }
    }
}