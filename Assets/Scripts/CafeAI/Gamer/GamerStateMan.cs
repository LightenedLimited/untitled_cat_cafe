using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class GamerStateMan : StateMan, ISeesCoffee
    {
        private Coffee targetCoffee;
        public Seatable Seat;
        // private Seatable Seat;
        // Sleeps if SleepTimer hits 0
        [SerializeField] public float SleepTimer = 30;
        public Coffee TargetCoffee{
            get => targetCoffee;
            set => targetCoffee = value;
        }


        // May need reference to Laptop
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
        }
    }
}