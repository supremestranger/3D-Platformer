using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSystem : IEcsRunSystem
{
    private EcsFilter<HitComponent> hits;
    private Startup startup;

    public void Run()
    {
        foreach(var i in hits)
        {
            if (!startup.playerEntity.IsAlive())
            {
                return;
            }
            ref var hitComponent = ref hits.Get1(i);
            ref var playerComponent = ref startup.playerEntity.Get<PlayerComponent>();

            if (hitComponent.other.CompareTag("Coin"))
            {
                hitComponent.other.gameObject.SetActive(false);
                playerComponent.coins += 1;
                startup.coinCounter.text = playerComponent.coins.ToString();
            }

            if (hitComponent.other.CompareTag("BadCoin"))
            {
                hitComponent.other.gameObject.SetActive(false);
                playerComponent.coins -= 1;
                startup.coinCounter.text = playerComponent.coins.ToString();
            }

            if (hitComponent.other.CompareTag("Dangerous"))
            {
                startup.playerEntity.Get<PlayerComponent>().playerTransform.gameObject.SetActive(false);
                startup.playerEntity.Destroy();
                startup.gameOverPanel.SetActive(true);
            }

            if (hitComponent.other.CompareTag("WinPoint"))
            {
                startup.playerEntity.Get<PlayerComponent>().playerTransform.gameObject.SetActive(false);
                startup.playerEntity.Destroy();
                startup.playerWonPanel.SetActive(true);
            }

            if (hitComponent.other.CompareTag("JumpBuff"))
            {
                hitComponent.other.gameObject.SetActive(false);
                startup.playerEntity.Get<PlayerComponent>().playerJumpForce *= 2f;
                ref var jumpBuffComponent = ref startup.playerEntity.Get<JumpBuffComponent>();
                jumpBuffComponent.timer = startup.Configuration.jumpBuffDuration;
            }

            if (hitComponent.other.CompareTag("SpeedBuff"))
            {
                hitComponent.other.gameObject.SetActive(false);
                startup.playerEntity.Get<PlayerComponent>().playerSpeed *= 2f;
                ref var speedBuffComponent = ref startup.playerEntity.Get<SpeedBuffComponent>();
                speedBuffComponent.timer = startup.Configuration.speedBuffDuration;
            }
        }
    }
}
