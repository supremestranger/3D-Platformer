using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;

    public void Init()
    {
        foreach(var i in GameObject.FindGameObjectsWithTag("Dangerous"))
        {
            var dangerousEntity = ecsWorld.NewEntity();

            ref var dangerousComponent = ref dangerousEntity.Get<DangerousComponent>();

            dangerousComponent.obstacleTransform = i.transform;
            dangerousComponent.pointA = i.transform.Find("A").position;
            dangerousComponent.pointB = i.transform.Find("B").position;
        }
    }
}
