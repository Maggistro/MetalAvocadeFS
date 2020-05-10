using System;
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

            SetRandomSprite();
        }

        protected override void TriggerEntered()
        {
            base.TriggerEntered();
            if (navCollider != null)
            {
                movementDirection = -movementDirection;
            }
        }

        private void SetRandomSprite()
        {
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            if (sprites.Length > 0)
            {

                System.Random rng = new System.Random(Guid.NewGuid().GetHashCode());
                int randIndex = rng.Next(0, sprites.Length);
                foreach (SpriteRenderer sr in sprites)
                {
                    sr.enabled = false;
                }
                sprites[randIndex].enabled = true;
            }
        }
    }
}