using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{ 
    public class CharacterSpammer : MonoBehaviour
    {
        public GameObject babyTemplate;
        [SerializeField]
        private int numberOfBabies;
        public GameObject touristTemplate;
        [SerializeField]
        private int numberOfTourits;
        private BoxCollider groundCollider;
        private LayerMask groundMask;
        private Bounds groundBounds;
        private System.Random rng;
        private Transform sceneParent;
        

        void Start()
        {
            rng = new System.Random();

            GameObject npcParentGO = new GameObject("NpcCharacters");
            sceneParent = npcParentGO.transform;
            sceneParent.position = new Vector3(0f, 0f, 0f);
            sceneParent.rotation = Quaternion.identity;
            sceneParent.localScale = new Vector3(1f, 1f, 1f);

            groundMask = LayerMask.NameToLayer("ground");
            groundCollider = GetGroundCollider();
            if (groundCollider != null)
            {
                groundBounds = groundCollider.bounds;
            }

            SpawnNpcsFromTemplate(babyTemplate);
            SpawnNpcsFromTemplate(touristTemplate);
        }
        
        private BoxCollider GetGroundCollider()
        {
            BoxCollider result = new BoxCollider();
            if (Camera.main != null)
            {
                Ray groundRay = new Ray(new Vector3(0f, 50f, 0f), new Vector3(0f, -1f, 0f) * 200f);
                RaycastHit raycastHit;
                if (Physics.Raycast(groundRay, out raycastHit, 200f))
                {
                    //Debug.DrawRay(groundRay.origin, groundRay.direction * 200f, Color.red, 15f);
                    if (raycastHit.collider.gameObject.layer == groundMask)
                    {
                        result = (BoxCollider)raycastHit.collider;
                    }
                }
            }
            return result;
        }

        private void SpawnNpcsFromTemplate(GameObject template)
        {
            if (template != null)
            {
                for (int i = 0; i < numberOfBabies; i++)
                {
                    GameObject npc = Instantiate(template, 
                        GetRandomPositionOnGround(), Quaternion.identity) as GameObject;
                    npc.transform.parent = sceneParent;
                    SetRBValuesToZero(npc);
                    CheckPosition(npc.transform);
                }
            }
        }
        
        private Vector3 GetRandomPositionOnGround()
        {
            Vector3 position = Vector3.zero;
            if (groundBounds != null && groundCollider != null)
            {
                float offsetX = groundBounds.extents.x * 0.75f;
                float offsetZ = groundBounds.extents.z * 0.75f;
                float x = ((float)rng.NextDouble() * offsetX * 2) - offsetX;
                float y = groundCollider.transform.position.y + groundCollider.size.y;
                float z = ((float)rng.NextDouble() * offsetZ * 2) - offsetZ;
                return new Vector3(x + groundCollider.transform.position.x, y, 
                    z + groundCollider.transform.position.z);
            }
            else
            {
                return position;
            }
        }

        private void SetRBValuesToZero(GameObject go)
        {
            Rigidbody rb = go.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = new Vector3(0f, 0f, 0f);
                rb.angularVelocity = new Vector3(0f, 0f, 0f);
            }
        }

        // Use a Raycast to check if the transform has been positioned in another rigidbody.
        private void CheckPosition(Transform transformToCheck)
        {
            Rigidbody rb = transformToCheck.GetComponent<Rigidbody>();
            float testDistance = 2f;
            RaycastHit raycastHit;

            if (rb != null)
            {
                if (rb.SweepTest(transformToCheck.forward, out raycastHit, testDistance))
                {
                    transformToCheck.position += new Vector3(0f, 0f, -1f);
                }
                if (rb.SweepTest(- transformToCheck.forward, out raycastHit, testDistance))
                {
                    transformToCheck.position += new Vector3(0f, 0f, 1f);
                }
                if (rb.SweepTest(transformToCheck.right, out raycastHit, testDistance))
                {
                    transformToCheck.position += new Vector3(-1f, 0f, 0f);
                }
                if (rb.SweepTest(- transformToCheck.right, out raycastHit, testDistance))
                {
                    transformToCheck.position += new Vector3(1f, 0f, 0f);
                }
            }
        }
    }
}
