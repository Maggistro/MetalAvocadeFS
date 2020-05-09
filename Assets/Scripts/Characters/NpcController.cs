using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NpcController : MonoBehaviour
{
    protected string npcName;

    [SerializeField]
    protected float movementSpeed;
    protected Vector3 movementDirection = Vector3.forward;
    protected System.Random rng;
    [SerializeField]
    protected float npcDecisionTimer = 1f;

    protected float maxNavSearchDistance = 5f;

    protected void Awake()
    {
        //TODO setup
        rng = new System.Random();
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


    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, npcName);
    }
}
