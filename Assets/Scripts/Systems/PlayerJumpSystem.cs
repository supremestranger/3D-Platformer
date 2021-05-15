using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class PlayerJumpSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private EcsFilter<PlayerComponent, PlayerInputComponent, GroundedComponent> playerFilter;
        private EcsFilter<TryJump> tryJumpFilter;

        public void Run()
        {
            foreach (var i in tryJumpFilter)
            {
                tryJumpFilter.GetEntity(i).Del<TryJump>();
                foreach (var j in playerFilter)
                {
                    ref var playerComponent = ref playerFilter.Get1(j);
                    ref var playerInputComponent = ref playerFilter.Get2(j);

                    playerComponent.playerRB.AddForce(Vector3.up * playerComponent.playerJumpForce, ForceMode.VelocityChange);
                }
            }
        }
    }
}