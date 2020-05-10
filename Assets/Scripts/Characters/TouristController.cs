using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class TouristController : NpcController
    {
        private new void Awake()
        {
            base.Awake();
            movementDirection = new Vector3(0f, 0f, 1f);
            RandomizeDecisionTimer();
        }

        protected override void TriggerEntered()
        {
            base.TriggerEntered();
            if (navCollider != null)
            {
                movementDirection = -movementDirection;
            }
        }
    }
}