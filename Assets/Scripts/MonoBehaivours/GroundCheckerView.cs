using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GroundCheckerView : MonoBehaviour
    {
        public EcsEntity playerEntity { get; set; }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                playerEntity.Get<GroundedComponent>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                playerEntity.Del<GroundedComponent>();
            }
        }
    }
}