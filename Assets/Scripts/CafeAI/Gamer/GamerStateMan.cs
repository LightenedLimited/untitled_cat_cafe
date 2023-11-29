using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AI;

namespace CatCafeAI
{
    public class GamerStateMan : StateMan, ISeesCoffee
    {
        // TODO: consider moving this to an interface?
        private Laptop laptop;
        public Laptop Laptop => laptop;
        public Seatable seat;
        public Seatable Seat => seat;
        // private Seatable Seat;
        // Sleeps if Alertness hits 0
        [SerializeField] public State NoSeatTransition;
        [SerializeField] public float MaxAlertness = 60;
        [SerializeField] public float NoSeatDistance = 1f;
        [SerializeField] public float CoffeeAlertness = 10;
        public float Alertness;
        
        public void Sit(Seatable s){
            if (seat is null)
                seat = s;
        }

        // Claims an arbritary laptop, regardless of distance.
        // TODO: safety, range
        public void GetLaptop()
        {
            Laptop[] laptops = FindObjectsByType<Laptop>(FindObjectsSortMode.None);
            laptop = laptops.Length == 0? null : laptops[0];
            // if Vector3.Distance(laptop.transform.position, transform.position) > 
        }
        public Coffee TargetCoffee{
            get => targetCoffee;
            set => targetCoffee = value;
        }

        private Coffee targetCoffee;

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            Alertness = MaxAlertness;
        }

        void Update()
        {
            Alertness -= Time.deltaTime;
            if ((seat == null || Vector3.Distance(seat.SeatPosition, transform.position) >= NoSeatDistance) 
                && (ActiveState != NoSeatTransition))
            {
                base.Transition(ActiveState, NoSeatTransition);
            }
        }

        public void DrinkCoffee()
        {
            Alertness += CoffeeAlertness;
        }
    }
}