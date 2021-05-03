using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer
{
    public class SpeedBuffSystem : IEcsRunSystem
    {
        private EcsFilter<SpeedBuffComponent, PlayerComponent> speedBuff;

        public void Run()
        {
            foreach (var i in speedBuff)
            {
                ref var speedBuffComponent = ref speedBuff.Get1(i);
                ref var playerComponent = ref speedBuff.Get2(i);

                speedBuffComponent.timer -= Time.deltaTime;

                if (speedBuffComponent.timer <= 0)
                {
                    playerComponent.playerSpeed /= 2f;
                    speedBuff.GetEntity(i).Del<SpeedBuffComponent>();
                }
            }
        }
    }
}