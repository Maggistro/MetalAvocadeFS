using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Vector2 movementVector;
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            var a = Input.GetAxis("Horizontal");
            Debug.Log(a);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            var b = Input.GetAxis("Vertical");
            Debug.Log(b);
        }
    }
}
