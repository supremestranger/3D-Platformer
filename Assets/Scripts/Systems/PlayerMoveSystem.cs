using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private EcsFilter<PlayerComponent, PlayerInputComponent> playerFilter;

        public void Run()
        {
            foreach (var i in playerFilter)
            {
                ref var playerComponent = ref playerFilter.Get1(i);
                ref var playerInputComponent = ref playerFilter.Get2(i);

                playerComponent.playerRB.AddForce(playerInputComponent.moveInput * playerComponent.playerSpeed, ForceMode.Acceleration);
            }
            
        }
    }
}