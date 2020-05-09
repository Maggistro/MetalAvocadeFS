using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

public class NpcController : MonoBehaviour
{
    protected string npcName;

    [SerializeField]
    protected float movementSpeed;
    protected Vector3 movementDirection = Vector3.forward;
    protected System.Random rng;
    [SerializeField]
    protected float npcDecisionTimer = 5f;

    protected float maxNavSearchDistance = 5f;

    protected SphereCollider navCollider;

    protected void Awake()
    {
        rng = new System.Random();

        navCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if ((Time.timeSinceLevelLoad % npcDecisionTimer) <= 0.01f)
        {
            EvaluateMovement();
        }

        Move();
    }


    protected virtual void EvaluateMovement()
    {
        float x = rng.Next((int)-maxNavSearchDistance, (int)maxNavSearchDistance);
        float z = rng.Next((int)-maxNavSearchDistance, (int)maxNavSearchDistance);

        movementDirection = new Vector3(x, 0, z);
    }


    protected virtual void Move()
    {
        transform.forward = movementDirection;
        transform.position = Vector3.Lerp(transform.position, 
            transform.position + transform.forward * movementSpeed, Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (navCollider != null && !other.isTrigger)
        {
            EvaluateMovement();
        }
    }


    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, npcName);
    }
}
