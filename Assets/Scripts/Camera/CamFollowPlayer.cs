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
            transform.position = new Vector3(player.position.x - offset, player.position.y + offset, player.position.z - offset);
        }
    }
}