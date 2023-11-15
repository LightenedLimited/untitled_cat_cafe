using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatCafeAI
{
    public class FrustratedState : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] State FinishedState;
        [SerializeField] State OutOfPatienceState;
        [SerializeField] float FrustrationLength; 
        private NavMeshAgent agent;
        private IPatience patienceMan;
        private float FrustrationTime;
        void Start()
        {
            base.Start();
            if (!TryGetComponent<NavMeshAgent>(out agent))
                Debug.LogError("No Navmesh Agent");

            if (manager is not ISeesCoffee)
                Debug.LogError("Statemanager needs to see coffee");
            else
                patienceMan = (IPatience)manager;
        }

        void OnEnable()
        {
            FrustrationTime = FrustrationLength;
            patienceMan.Patience--;
        }
        // Update is called once per frame
        void Update()
        {
            if (patienceMan.Patience == 0)
                manager.Transition();
        }
    }

}