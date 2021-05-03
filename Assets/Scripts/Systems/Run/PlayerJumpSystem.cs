using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpSystem : IEcsRunSystem
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

        if (playerInputComponent.jumpInput)
        {
            playerInputComponent.jumpInput = false;
            if (startup.playerEntity.Has<GroundedComponent>())
            {
                playerComponent.playerRB.AddForce(Vector3.up * playerComponent.playerJumpForce, ForceMode.VelocityChange);
            }
        }
    }
}
