using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Avocado
{
    public class CharacterController : NpcController
    {
        protected bool active = true;
        public bool SetActivestate { set { active = value; } }
        [Header("Health")]
        [SerializeField] private float healthMax = 100;
        [SerializeField] private float health;
        public float HealthLevel { get { return health / healthMax; } }
        [Header("Stamina")]
        [SerializeField] private float staminaMax = 10;
        [SerializeField] private float stamina = 10;
        public float StaminaLevel { get { return stamina / staminaMax; } }
        [Header("Water")]
        [SerializeField] private float waterAmountMax;
        [SerializeField] private float waterAmount = 0;
        public float WaterLevel { get { return waterAmount / waterAmountMax; } }
        public float AddWater { set { waterAmount += value; } }
        [Header("Movement")]
        [SerializeField] private Collider footCollider;
        [SerializeField] private float moveSpeedMax = 16f;
        [SerializeField] private float moveSpeed;
        [SerializeField] Vector2 movementVector = Vector2.zero;
        [SerializeField] private Vector3 forward;
        [SerializeField] private Vector3 right;
        [SerializeField] private float jumpForce = 250f;
        [SerializeField] private bool grounded;
        public float distToGround = 1;
        private int layerMask = 1 << 8;//ground
        private Rigidbody rb;
        private LinkedList<JokerArt> availableArt;
        private Boat availableBoat;
        public virtual void Start()
        {
            health = healthMax;
            moveSpeed = moveSpeedMax / 2;
            availableArt = new LinkedList<JokerArt>();
            rb = GetComponent<Rigidbody>();
            forward = Camera.main.transform.forward; // Set forward to equal the camera's forward vector
            forward.y = 0; // make sure y is 0
            forward = Vector3.Normalize(forward); // make sure the length of vector is set to a max of 1.0
            right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // set the right-facing vector to be facing right relative to the camera's forward vector
        }
        public new virtual void Update()
        {
            base.Update();
            if (!active)
                return;
            if (Input.GetKey(KeyCode.LeftShift) && IsGrounded())
            {
                if (stamina > 0)
                {
                    moveSpeed = moveSpeedMax;
                    stamina -= Time.deltaTime;
                }
                else
                {
                    moveSpeed = moveSpeedMax / 2;
                    stamina += Time.deltaTime / 2;
                }
            }
            else
            {
                moveSpeed = moveSpeedMax / 2;
                stamina += Time.deltaTime / 2;
            }
            if (stamina > staminaMax)
                stamina = staminaMax;
            // Debug.DrawRay(transform.position, -Vector3.up * (distToGround + .1f), Color.red, 1f);
            if (Input.anyKey)
            {
                Move();
            }
            if (Input.GetKeyDown("space") && IsGrounded())
                Jump();
            if (Input.GetKeyDown(KeyCode.E) && IsGrounded())
            {
                if (availableArt.Count > 0)
                {
                    Clean();
                    return;
                }
                if (availableBoat != null)
                {
                    availableBoat.Use(this.transform);
                }
            }

        }
        public new virtual void Move()
        {
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"); // Our right movement is based on the right vector, movement speed, and our GetAxis command. We multiply by Time.deltaTime to make the movement smooth.
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"); // Up movement uses the forward vector, movement speed, and the vertical axis inputs.
            Vector3 heading = Vector3.Normalize(rightMovement + upMovement); // This creates our new direction. By combining our right and forward movements and normalizing them, we create a new vector that points in the appropriate direction with a length no greater than 1.0
            movementDirection = heading; // update npc controller direction
            transform.forward = heading; // Sets forward direction of our game object to whatever direction we're moving in
            transform.position += rightMovement; // move our transform's position right/left
            transform.position += upMovement; // Move our transform's position up/down
        }

        void Jump()
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
        }
        public bool IsGrounded()
        {
            return Physics.Raycast(footCollider.transform.position, -Vector3.up, distToGround + 0.1f, layerMask);
        }
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "WaterPickup")
            {
                Waterdroplet droplet = collider.GetComponent<Waterdroplet>();
                AddWater = droplet.Value;
                droplet.Value = -droplet.Value;
            }
            if (collider.tag == "JokerArt")
            {
                if (!availableArt.Contains(collider.GetComponent<JokerArt>()))
                {
                    availableArt.AddLast(collider.GetComponent<JokerArt>());
                }
            }
            if (collider.tag == "Boat")
            {
                availableBoat = collider.GetComponent<Boat>();
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.tag == "JokerArt")
            {
                availableArt.Remove(collider.GetComponent<JokerArt>());
            }
        }
        private void Clean()
        {
            JokerArt art = GetClosestArt();
            if (art != null)
            {
                if (WaterLevelCk(art.Value))
                {
                    AddWater = -art.Value;
                    availableArt.Remove(art);
                    art.Value = -art.Value;
                }
            }
        }

        private bool WaterLevelCk(float value)
        {
            return (waterAmount - value) >= 0 ? true : false;
        }

        private JokerArt GetClosestArt()
        {
            float dist = Mathf.Infinity;
            JokerArt closestArt = null;
            foreach (JokerArt art in availableArt)
            {
                float tempDist = Vector3.Distance(transform.position, art.transform.position);
                if (tempDist < dist)
                {
                    dist = tempDist;
                    closestArt = art;
                }
            }
            return closestArt;
        }


        public void PickupBroom()
        {
            Debug.Log("Picking up broom");
        }
    }
}