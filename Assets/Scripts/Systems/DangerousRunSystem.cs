using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class DangerousRunSystem : IEcsRunSystem
    {
        // auto-injected fields
        private EcsFilter<DangerousComponent> dangerousObstacles;

        public void Run()
        {
            foreach (var i in dangerousObstacles)
            {
                ref var dangerousComponent = ref dangerousObstacles.Get1(i);
                Vector3 pos1 = dangerousComponent.pointA;
                Vector3 pos2 = dangerousComponent.pointB;

                dangerousComponent.obstacleTransform.localPosition = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time, 1.0f));
            }
        }
    }
}