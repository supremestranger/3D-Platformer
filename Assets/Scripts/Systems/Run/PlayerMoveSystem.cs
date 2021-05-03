using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
    private Startup startup;

    public void Run()
    {
        if (!startup.playerEntity.IsAlive())
        {
            return;
        }
        ref var playerComponent = ref startup.playerEntity.Get<PlayerComponent>();
        ref var playerInputComponent = ref startup.playerEntity.Get<PlayerInputComponent>();

        playerComponent.playerRB.AddForce(playerInputComponent.moveInput * playerComponent.playerSpeed, ForceMode.Acceleration);
    }
}
