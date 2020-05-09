using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class Boat : CharacterController
    {
        private int layerMaskBoat = 1 << 4;//water
        bool stickPlayer = false;
        Transform passenger;

        public override void Start()
        {
            base.Start();
            distToGround *= 10;
        }

        public override void Update()
        {
            if (!active)
                return;

            float offset = 2;
            Vector3 right = new Vector3(transform.position.x + offset, transform.position.y + offset * 3, transform.position.z);
            Vector3 left = new Vector3(transform.position.x - offset, transform.position.y + offset * 3, transform.position.z);
            Vector3 up = new Vector3(transform.position.x, transform.position.y + offset * 3, transform.position.z + offset);
            Vector3 down = new Vector3(transform.position.x, transform.position.y + offset * 3, transform.position.z - offset);

            List<Vector3> exitPoints = new List<Vector3>();
            Debug.DrawRay(right, Vector3.down * (distToGround), Color.red, 1f);
            Debug.Log("Right: " + Physics.Raycast(right, Vector3.down, distToGround, layerMaskBoat));
            if (!Physics.Raycast(right, Vector3.down, distToGround, layerMaskBoat))
            {
                exitPoints.Add(right);
            }

            Debug.DrawRay(left, Vector3.down * (distToGround), Color.red, 1f);
            Debug.Log("Left: " + Physics.Raycast(left, Vector3.down, distToGround, layerMaskBoat));
            if (!Physics.Raycast(left, Vector3.down, distToGround, layerMaskBoat))
            {
                exitPoints.Add(left);
            }
            Debug.DrawRay(up, Vector3.down * (distToGround), Color.red, 1f);
            Debug.Log("Up: " + Physics.Raycast(up, Vector3.down, distToGround, layerMaskBoat));
            if (!Physics.Raycast(up, Vector3.down, distToGround, layerMaskBoat))
            {
                exitPoints.Add(up);
            }
            Debug.DrawRay(down, Vector3.down * (distToGround), Color.red, 1f);
            Debug.Log("Down: " + Physics.Raycast(down, Vector3.down, distToGround, layerMaskBoat));
            if (!Physics.Raycast(down, Vector3.down, distToGround, layerMaskBoat))
            {
                exitPoints.Add(down);
            }

            if (stickPlayer)
                passenger.position = transform.position;

            if (Input.anyKey)
            {
                Move();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Exit(exitPoints);
            }
        }
        public void Use(Transform passenger)
        {
            this.passenger = passenger;
            stickPlayer = true;
            passenger.GetComponent<CharacterController>().SetActivestate = false;
            SetActivestate = true;
        }
        private void Exit(List<Vector3> exits)
        {
            if (exits.Count > 0)
            {
                Vector3 tempPos = exits[Random.Range(0, exits.Count)];
                stickPlayer = false;
                SetActivestate = false;
                passenger.position = new Vector3(tempPos.x, 1, tempPos.z);
                passenger.GetComponent<CharacterController>().SetActivestate = true;
                // passenger = null;
            }
        }
    }
}