using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugging : MonoBehaviour
{
    public bool debug;

    void Update()
    {
        if (debug)
        {
            if (Input.GetKeyDown("o"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().AddWater = 1;
            }
            if (Input.GetKeyDown("l"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().AddWater = -1;
            }
        }
    }
}
