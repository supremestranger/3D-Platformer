using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class HitSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private GameData gameData;
        private EcsFilter<HitComponent> hits;
        private EcsFilter<PlayerComponent> playerFilter;

        public void Run()
        {
            foreach (var i in hits)
            {
                ref var hitComponent = ref hits.Get1(i);

                foreach(var j in playerFilter)
                {
                    ref var playerComponent = ref playerFilter.Get1(i);

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
                        playerComponent.playerTransform.gameObject.SetActive(false);
                        playerFilter.GetEntity(i).Destroy();
                        gameData.gameOverPanel.SetActive(true);
                    }

                    if (hitComponent.other.CompareTag("WinPoint"))
                    {
                        playerComponent.playerTransform.gameObject.SetActive(false);
                        playerFilter.GetEntity(i).Destroy();
                        gameData.playerWonPanel.SetActive(true);
                    }

                    if (hitComponent.other.CompareTag("JumpBuff"))
                    {
                        hitComponent.other.gameObject.SetActive(false);
                        playerComponent.playerJumpForce *= 2f;
                        ref var jumpBuffComponent = ref playerFilter.GetEntity(i).Get<JumpBuffComponent>();
                        jumpBuffComponent.timer = gameData.configuration.jumpBuffDuration;
                    }

                    if (hitComponent.other.CompareTag("SpeedBuff"))
                    {
                        hitComponent.other.gameObject.SetActive(false);
                        playerComponent.playerSpeed *= 2f;
                        ref var speedBuffComponent = ref playerFilter.GetEntity(i).Get<SpeedBuffComponent>();
                        speedBuffComponent.timer = gameData.configuration.speedBuffDuration;
                    }
                }
                
            }
        }
    }
}