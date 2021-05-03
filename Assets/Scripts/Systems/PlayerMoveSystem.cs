using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private GameData gameData;

        public void Run()
        {
            if (!gameData.playerEntity.IsAlive())
            {
                return;
            }
            ref var playerComponent = ref gameData.playerEntity.Get<PlayerComponent>();
            ref var playerInputComponent = ref gameData.playerEntity.Get<PlayerInputComponent>();

            playerComponent.playerRB.AddForce(playerInputComponent.moveInput * playerComponent.playerSpeed, ForceMode.Acceleration);
        }
    }
}