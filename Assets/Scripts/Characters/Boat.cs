using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class Boat : CharacterController
    {
        bool stickPlayer = false;
        Transform passenger;
        private void Update()
        {
            if (stickPlayer)
                passenger.position = transform.position;
        }
        public void Use(Transform passenger)
        {
            this.passenger = passenger;
            stickPlayer = true;
            passenger.GetComponent<CharacterController>().SetActivestate = false;
            SetActivestate = true;
        }
    }
}