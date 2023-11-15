using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CatCafeAI
{
    public class CustomerStateMan : StateMan, ISeesCoffee
    {
        private NavMeshAgent agent;
        protected override void Start()
        {
            base.Start();
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
        }
        private Interactable targetCoffee;
        public Interactable TargetCoffee{
            get => targetCoffee;
            set => targetCoffee = value;
        }
    }
}