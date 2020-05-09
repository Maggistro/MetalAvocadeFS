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
            npcName = "Tourist";
            movementDirection = new Vector3(0f, 0f, 1f);
        }

        protected override void TriggerEntered()
        {
            if (navCollider != null)
            {
                movementDirection = -movementDirection;
            }
        }
    }
}