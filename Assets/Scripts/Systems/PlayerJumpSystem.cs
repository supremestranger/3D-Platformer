using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class PlayerJumpSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private GameData gameData;
        private EcsFilter<PlayerComponent, PlayerInputComponent, GroundedComponent> playerFilter;

        public void Run()
        {
            foreach (var i in playerFilter)
            {
                ref var playerComponent = ref playerFilter.Get1(i);
                ref var playerInputComponent = ref playerFilter.Get2(i);

                if (playerInputComponent.jumpInput)
                {
                    playerInputComponent.jumpInput = false;
                    playerComponent.playerRB.AddForce(Vector3.up * playerComponent.playerJumpForce, ForceMode.VelocityChange);
                }
            }
        }
    }
}