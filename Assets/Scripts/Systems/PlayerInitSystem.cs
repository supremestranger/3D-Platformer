using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        // auto-injected fields.
        private EcsWorld ecsWorld;
        private GameData gameData;

        public void Init()
        {
            var playerEntity = ecsWorld.NewEntity();

            ref var playerComponent = ref playerEntity.Get<PlayerComponent>();
            ref var playerInputComponent = ref playerEntity.Get<PlayerInputComponent>();

            var playerGO = GameObject.FindGameObjectWithTag("Player");

            playerGO.GetComponentInChildren<GroundCheckerView>().playerEntity = playerEntity;
            playerGO.GetComponentInChildren<CollisionCheckerView>().ecsWorld = ecsWorld;
            playerComponent.playerSpeed = gameData.configuration.playerSpeed;
            playerComponent.playerTransform = playerGO.transform;
            playerComponent.playerJumpForce = gameData.configuration.playerJumpForce;
            playerComponent.playerCollider = playerGO.GetComponent<CapsuleCollider>();
            playerComponent.playerRB = playerGO.GetComponent<Rigidbody>();
        }
    }
}