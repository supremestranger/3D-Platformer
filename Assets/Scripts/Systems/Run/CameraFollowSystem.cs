using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSystem : IEcsRunSystem
{
    private Startup startup;

    public void Run()
    {
        if(!startup.playerEntity.IsAlive() || !startup.cameraEntity.IsAlive())
        {
            return;
        }
        ref var cameraComponent = ref startup.cameraEntity.Get<CameraComponent>();
        ref var playerComponent = ref startup.playerEntity.Get<PlayerComponent>();

        Vector3 currentPosition = cameraComponent.cameraTransform.position;
        Vector3 targetPoint = playerComponent.playerTransform.position + cameraComponent.offset;

        cameraComponent.cameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.curVelocity, cameraComponent.cameraSmoothness);
    }
}
