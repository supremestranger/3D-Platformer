using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class HitSystem : IEcsRunSystem
    {
        private EcsFilter<HitComponent> hits;
        private GameData gameData;

        public void Run()
        {
            foreach (var i in hits)
            {
                if (!gameData.playerEntity.IsAlive())
                {
                    return;
                }
                ref var hitComponent = ref hits.Get1(i);
                ref var playerComponent = ref gameData.playerEntity.Get<PlayerComponent>();

                if (hitComponent.other.CompareTag("Coin"))
                {
                    hitComponent.other.gameObject.SetActive(false);
                    playerComponent.coins += 1;
                    gameData.coinCounter.text = playerComponent.coins.ToString();
                }

                if (hitComponent.other.CompareTag("BadCoin"))
                {
                    hitComponent.other.gameObject.SetActive(false);
                    playerComponent.coins -= 1;
                    gameData.coinCounter.text = playerComponent.coins.ToString();
                }

                if (hitComponent.other.CompareTag("Dangerous"))
                {
                    gameData.playerEntity.Get<PlayerComponent>().playerTransform.gameObject.SetActive(false);
                    gameData.playerEntity.Destroy();
                    gameData.gameOverPanel.SetActive(true);
                }

                if (hitComponent.other.CompareTag("WinPoint"))
                {
                    gameData.playerEntity.Get<PlayerComponent>().playerTransform.gameObject.SetActive(false);
                    gameData.playerEntity.Destroy();
                    gameData.playerWonPanel.SetActive(true);
                }

                if (hitComponent.other.CompareTag("JumpBuff"))
                {
                    hitComponent.other.gameObject.SetActive(false);
                    gameData.playerEntity.Get<PlayerComponent>().playerJumpForce *= 2f;
                    ref var jumpBuffComponent = ref gameData.playerEntity.Get<JumpBuffComponent>();
                    jumpBuffComponent.timer = gameData.configuration.jumpBuffDuration;
                }

                if (hitComponent.other.CompareTag("SpeedBuff"))
                {
                    hitComponent.other.gameObject.SetActive(false);
                    gameData.playerEntity.Get<PlayerComponent>().playerSpeed *= 2f;
                    ref var speedBuffComponent = ref gameData.playerEntity.Get<SpeedBuffComponent>();
                    speedBuffComponent.timer = gameData.configuration.speedBuffDuration;
                }
            }
        }
    }
}