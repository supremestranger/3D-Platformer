using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInitSystem : IEcsInitSystem
{
    private EcsWorld ecsWorld;
    private Startup startup;

    public void Init()
    {
        var cameraEntity = ecsWorld.NewEntity();

        ref var cameraComponent = ref cameraEntity.Get<CameraComponent>();

        cameraComponent.cameraTransform = Camera.main.transform;
        cameraComponent.cameraSmoothness = startup.Configuration.cameraFollowSmoothness;
        cameraComponent.curVelocity = Vector3.zero;
        cameraComponent.offset = new Vector3(0f, 1f, -9f);

        startup.cameraEntity = cameraEntity;
    }
}
