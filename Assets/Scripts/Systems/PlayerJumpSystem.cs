﻿using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class PlayerJumpSystem : IEcsRunSystem
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

            if (playerInputComponent.jumpInput)
            {
                playerInputComponent.jumpInput = false;
                if (gameData.playerEntity.Has<GroundedComponent>())
                {
                    playerComponent.playerRB.AddForce(Vector3.up * playerComponent.playerJumpForce, ForceMode.VelocityChange);
                }
            }
        }
    }
}