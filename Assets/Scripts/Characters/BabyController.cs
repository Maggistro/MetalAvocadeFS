using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class BabyController : NpcController
    {
        private bool isMoving = true;
        private float originalMovementSpeed;

        private new void Awake()
        {
            base.Awake();
            RandomizeDecisionTimer();
        }

        protected override void EvaluateMovement()
        {
            base.EvaluateMovement();

            if (navCollider != null)
            {
                if (WantsToMove())
                {
                    if (!isMoving)
                    {
                        SetMovingState(true);
                    }
                    SetNewMovementDirection();
                }
                else
                {
                    SetMovingState(false);
                }
            }
        }

        // Sets a new random movement direction for the npc. 
        private void SetNewMovementDirection()
        {
            float x = rng.Next(Mathf.FloorToInt(-navCollider.radius), Mathf.FloorToInt(navCollider.radius));
            if (x == 0)
            {
                x += navCollider.radius;
            }
            float z = rng.Next(Mathf.FloorToInt(-navCollider.radius), Mathf.FloorToInt(navCollider.radius));
            if (z == 0)
            {
                z += navCollider.radius;
            }

            movementDirection = new Vector3(x, 0, z);
        }

        // Lets Baby decide whether or not to take a break. 
        private bool WantsToMove()
        {
            float randomNumber = rng.Next(1, 2);
            return randomNumber % 2 == 0;
        }

        // Sets the speed of movement to 0 or restores original movement speed.
        private void SetMovingState(bool newState)
        {
            if (!isMoving)
            {
                movementSpeed = originalMovementSpeed;
            }
            else
            {
                originalMovementSpeed = movementSpeed;
                movementSpeed = 0f;
            }

            isMoving = newState;
        }

        protected override void TriggerEntered()
        {
            base.TriggerEntered();
            SetNewMovementDirection();
        }
    }
}
