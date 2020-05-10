using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Avocado
{
    public class CamFollowPlayer : MonoBehaviour
    {
        public float offset;
        [SerializeField] private Transform player;
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        private void Update()
        {
            float newX = Mathf.Clamp(player.position.x, -64.5f, Mathf.Infinity);
            transform.position = new Vector3(newX - offset, player.position.y + offset, transform.position.z);
        }
    }
}