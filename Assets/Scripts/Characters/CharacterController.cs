using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Avocado
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private Collider footCollider;
        public bool airborne = false;
        [SerializeField] private float waterLevelMax;
        [SerializeField] private float waterLevel = 0;
        public float WaterLevel { get { return waterLevel / waterLevelMax; } }
        public float AddWater { set { waterLevel += value; } }
        [SerializeField] private float moveSpeed = 4f;
        Vector2 movementVector = Vector2.zero;
        Rigidbody rb;
        private Vector3 forward;
        private Vector3 right;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            forward = Camera.main.transform.forward; // Set forward to equal the camera's forward vector
            forward.y = 0; // make sure y is 0
            forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
            right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // set the right-facing vector to be facing right relative to the camera's forward vector
        }
        void Update()
        {
            // Debug.DrawRay(transform.position, -Vector3.up * (distToGround + .1f), Color.red, 1f);
            IsGrounded();
            if (Input.anyKey)
            {
                Move();
            }
            if (Input.GetKeyDown("space") && IsGrounded())
                Jump();
        }
        void Move()
        {
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"); // Our right movement is based on the right vector, movement speed, and our GetAxis command. We multiply by Time.deltaTime to make the movement smooth.
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"); // Up movement uses the forward vector, movement speed, and the vertical axis inputs.
            Vector3 heading = Vector3.Normalize(rightMovement + upMovement); // This creates our new direction. By combining our right and forward movements and normalizing them, we create a new vector that points in the appropriate direction with a length no greater than 1.0
            transform.forward = heading; // Sets forward direction of our game object to whatever direction we're moving in
            transform.position += rightMovement; // move our transform's position right/left
            transform.position += upMovement; // Move our transform's position up/down
        }
        [SerializeField] private float jumpForce = 250f;
        void Jump()
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
        public bool grounded;
        [SerializeField] float distToGround;
        int layerMask = 1 << 8;
        bool IsGrounded()
        {
            return Physics.Raycast(footCollider.transform.position, -Vector3.up, distToGround + 0.1f, layerMask);
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "WaterPickup")
            {
                AddWater = collider.GetComponent<Waterdroplet>().Value;
                Destroy(collider.gameObject);
            }
        }

    }
}