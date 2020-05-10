using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

namespace Avocado
{
    public class NpcController : MonoBehaviour
    {
        [SerializeField]
        public float movementSpeed;
        public Vector3 movementDirection = Vector3.right;
        protected System.Random rng;
        [SerializeField]
        protected float npcDecisionTimer = 3f;
        protected float maxNavSearchDistance = 5f;
        protected SphereCollider navCollider;

        protected void Awake()
        {
            rng = new System.Random();

            navCollider = GetComponent<SphereCollider>();
        }

        protected void Update()
        {
            if ((Time.timeSinceLevelLoad % npcDecisionTimer) <= 0.001f)
            {
                EvaluateMovement();
            }

            Move();
        }

        protected virtual void EvaluateMovement()
        { }

        // Move NPC in movementDirection.
        protected void Move()
        {
            transform.forward = movementDirection;
            transform.position = Vector3.Lerp(transform.position,
                transform.position + transform.forward * movementSpeed, Time.deltaTime);
        }

        // Called once something enters the SphereCollider on the Npc. 
        private void OnTriggerEnter(Collider other)
        {
            if (navCollider != null && other.gameObject.layer != LayerMask.NameToLayer("ground"))
            {
                TriggerEntered();
            }
        }

        protected virtual void TriggerEntered()
        { }

        // Randomize the amount of time until the npc takes action.
        protected virtual void RandomizeDecisionTimer()
        {
            double newTimer = rng.NextDouble();
            npcDecisionTimer = Mathf.Clamp((float)newTimer, 0f, 6f);
        }
    }
}
