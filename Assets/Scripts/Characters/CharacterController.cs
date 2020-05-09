using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Vector2 movementVector = Vector2.zero;
    Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            movementVector = new Vector2(Input.GetAxis("Horizontal"), movementVector.y);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            movementVector = new Vector2(movementVector.x, Input.GetAxis("Vertical"));
        }
        Debug.Log(movementVector);
        rigidbody.AddForce(movementVector);
    }
}
