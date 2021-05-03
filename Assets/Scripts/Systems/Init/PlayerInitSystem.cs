using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;
    private Startup startup;

    public void Init()
    {
        var playerEntity = ecsWorld.NewEntity();

        ref var playerComponent = ref playerEntity.Get<PlayerComponent>();

        var playerGO = GameObject.FindGameObjectWithTag("Player");

        playerGO.GetComponentInChildren<GroundCheck>().playerEntity = playerEntity;
        playerGO.GetComponentInChildren<CollisionChecker>().ecsWorld = ecsWorld;
        playerComponent.playerSpeed = startup.Configuration.playerSpeed;
        playerComponent.playerTransform = playerGO.transform;
        playerComponent.playerJumpForce = startup.Configuration.playerJumpForce;
        playerComponent.playerCollider = playerGO.GetComponent<CapsuleCollider>();
        playerComponent.playerRB = playerGO.GetComponent<Rigidbody>();

        startup.playerEntity = playerEntity;
    }
}
