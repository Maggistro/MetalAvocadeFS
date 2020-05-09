using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

namespace Avocado
{
    public class NpcController : MonoBehaviour
    {
        protected string npcName;
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

        protected void Move()
        {
            transform.forward = movementDirection;
            transform.position = Vector3.Lerp(transform.position,
                transform.position + transform.forward * movementSpeed, Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (navCollider != null && other.gameObject.layer != LayerMask.NameToLayer("ground"))
            {
                TriggerEntered();
            }
        }

        protected virtual void TriggerEntered()
        { }

        private void OnDrawGizmos()
        {
            Handles.Label(transform.position, npcName);
        }
    }
}
