using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CollisionCheckerView : MonoBehaviour
    {
        // auto-injected fields.
        public EcsWorld ecsWorld { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            var hit = ecsWorld.NewEntity();

            ref var hitComponent = ref hit.Get<HitComponent>();

            hitComponent.first = transform.root.gameObject;
            hitComponent.other = collision.gameObject;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Coin") || other.CompareTag("BadCoin"))
            {
                // instantly destroy coin to avoid multiple OnTriggerEnter() calls.
                other.gameObject.SetActive(false); 
            }
            var hit = ecsWorld.NewEntity();

            ref var hitComponent = ref hit.Get<HitComponent>();

            hitComponent.first = transform.root.gameObject;
            hitComponent.other = other.gameObject;
        }
    }
}