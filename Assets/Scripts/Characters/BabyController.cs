using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class BabyController : NpcController
    {
        private new void Awake()
        {
            base.Awake();
            npcName = "Spreitz-Baby";
        }

        protected override void EvaluateMovement()
        {
            if (navCollider != null)
            {
                float x = rng.Next(Mathf.FloorToInt(-navCollider.radius), Mathf.FloorToInt(navCollider.radius));
                float z = rng.Next(Mathf.FloorToInt(-navCollider.radius), Mathf.FloorToInt(navCollider.radius));

                movementDirection = new Vector3(x, 0, z);
            }
        }
    }
}
