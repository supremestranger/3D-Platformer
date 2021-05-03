using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTimerSystem : IEcsRunSystem
{
    private Startup startup;

    public void Run()
    {
        if (!startup.playerEntity.IsAlive())
        {
            return;
        }
        if (startup.playerEntity.Has<SpeedBuffComponent>())
        {
            ref var speedBuffComponent = ref startup.playerEntity.Get<SpeedBuffComponent>();
            speedBuffComponent.timer -= Time.deltaTime;
            if (speedBuffComponent.timer <= 0)
            {
                startup.playerEntity.Del<SpeedBuffComponent>();
                ref var playerComponent = ref startup.playerEntity.Get<PlayerComponent>();
                playerComponent.playerSpeed /= 2;
            }
        }

        if (startup.playerEntity.Has<JumpBuffComponent>())
        {
            ref var jumpBuffComponent = ref startup.playerEntity.Get<JumpBuffComponent>();
            jumpBuffComponent.timer -= Time.deltaTime;
            if (jumpBuffComponent.timer <= 0)
            {
                startup.playerEntity.Del<JumpBuffComponent>();
                ref var playerComponent = ref startup.playerEntity.Get<PlayerComponent>();
                playerComponent.playerJumpForce /= 2;
            }
        }
    }
}
