using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private GameData gameData;

        public void Run()
        {
            if (!gameData.playerEntity.IsAlive() || !gameData.cameraEntity.IsAlive())
            {
                return;
            }
            ref var cameraComponent = ref gameData.cameraEntity.Get<CameraComponent>();
            ref var playerComponent = ref gameData.playerEntity.Get<PlayerComponent>();

            Vector3 currentPosition = cameraComponent.cameraTransform.position;
            Vector3 targetPoint = playerComponent.playerTransform.position + cameraComponent.offset;

            cameraComponent.cameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.curVelocity, cameraComponent.cameraSmoothness);
        }
    }
}