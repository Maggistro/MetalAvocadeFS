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
            groundBounds = groundCollider.bounds;

            SpawnNpcsFromTemplate(babyTemplate);
            SpawnNpcsFromTemplate(touristTemplate);
        }
        
        private BoxCollider GetGroundCollider()
        {
            BoxCollider result = new BoxCollider();
            if (Camera.main != null)
            {
                Ray groundRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward * Camera.main.farClipPlane);
                RaycastHit raycastHit;
                if (Physics.Raycast(groundRay, out raycastHit, 200f))
                {
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
                }
            }
        }
        
        private Vector3 GetRandomPositionOnGround()
        {
            Vector3 position = Vector3.zero;
            float x = ((float)rng.NextDouble() * groundBounds.min.x * 2) - groundBounds.min.x;
            float y = groundCollider.transform.position.y + groundCollider.size.y;
            float z = ((float)rng.NextDouble() * groundBounds.min.z* 2) - groundBounds.min.z;
            return new Vector3(x, y, z);
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
    }
}
