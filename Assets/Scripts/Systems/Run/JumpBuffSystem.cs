using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class JumpBuffSystem : IEcsRunSystem
    {
        private EcsFilter<JumpBuffComponent, PlayerComponent> jumpBuff;

        public void Run()
        {
            foreach (var i in jumpBuff)
            {
                ref var jumpBuffComponent = ref jumpBuff.Get1(i);
                ref var playerComponent = ref jumpBuff.Get2(i);

                jumpBuffComponent.timer -= Time.deltaTime;

                if (jumpBuffComponent.timer <= 0)
                {
                    playerComponent.playerJumpForce /= 2f;
                    jumpBuff.GetEntity(i).Del<JumpBuffComponent>();
                }
            }
        }
    }

}
