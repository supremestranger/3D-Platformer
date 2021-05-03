using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld ecsWorld;
        private GameData gameData;

        public void Init()
        {
            var playerEntity = ecsWorld.NewEntity();

            ref var playerComponent = ref playerEntity.Get<PlayerComponent>();

            var playerGO = GameObject.FindGameObjectWithTag("Player");

            playerGO.GetComponentInChildren<GroundCheck>().playerEntity = playerEntity;
            playerGO.GetComponentInChildren<CollisionCheckerView>().ecsWorld = ecsWorld;
            playerComponent.playerSpeed = gameData.configuration.playerSpeed;
            playerComponent.playerTransform = playerGO.transform;
            playerComponent.playerJumpForce = gameData.configuration.playerJumpForce;
            playerComponent.playerCollider = playerGO.GetComponent<CapsuleCollider>();
            playerComponent.playerRB = playerGO.GetComponent<Rigidbody>();

            gameData.playerEntity = playerEntity;
        }
    }
}