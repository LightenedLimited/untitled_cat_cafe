using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace CatCafeAI
{
    public class ChildStateMan : StateMan, IPatience
    {
        private NavMeshAgent agent;
        [SerializeField]
        private int patience = 3;
        public int Patience 
        {
            get => patience;
            set => patience = value;
        }

        protected override void Awake()
        {
            base.Awake();
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");
        }
    }
}