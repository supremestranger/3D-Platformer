using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        // auto-injected fields.
        private EcsWorld ecsWorld;
        private GameData gameData;
        private EcsEntity cameraEntity;
        private EcsFilter<PlayerComponent> playerFilter;

        public void Init()
        {
            var cameraEntity = ecsWorld.NewEntity();

            ref var cameraComponent = ref cameraEntity.Get<CameraComponent>();

            cameraComponent.cameraTransform = Camera.main.transform;
            cameraComponent.cameraSmoothness = gameData.configuration.cameraFollowSmoothness;
            cameraComponent.curVelocity = Vector3.zero;
            cameraComponent.offset = new Vector3(0f, 1f, -9f);

            this.cameraEntity = cameraEntity;
        }

        public void Run()
        {
            ref var cameraComponent = ref cameraEntity.Get<CameraComponent>();

            foreach(var i in playerFilter)
            {
                ref var playerComponent = ref playerFilter.Get1(i);

                Vector3 currentPosition = cameraComponent.cameraTransform.position;
                Vector3 targetPoint = playerComponent.playerTransform.position + cameraComponent.offset;

                cameraComponent.cameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.curVelocity, cameraComponent.cameraSmoothness);
            }    
        }
    }
}