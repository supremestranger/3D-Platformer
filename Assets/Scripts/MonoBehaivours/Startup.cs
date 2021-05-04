using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class Startup : MonoBehaviour
    {
        private EcsWorld ecsWorld;
        private EcsSystems initSystems;
        private EcsSystems updateSystems;
        private EcsSystems fixedUpdateSystems;
        [SerializeField] private ConfigurationSO configuration;
        [SerializeField] private Text coinCounter;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject playerWonPanel;

        private void Start()
        {
            ecsWorld = new EcsWorld();
            var gameData = new GameData();

            gameData.configuration = configuration;
            gameData.coinCounter = coinCounter;
            gameData.gameOverPanel = gameOverPanel;
            gameData.playerWonPanel = playerWonPanel;

            initSystems = new EcsSystems(ecsWorld)
                .Add(new PlayerInitSystem())
                .Add(new DangerousInitSystem())
                .Inject(gameData);
            updateSystems = new EcsSystems(ecsWorld)
                .Add(new PlayerInputSystem())
                .Add(new DangerousRunSystem())
                .Add(new HitSystem())
                .Add(new SpeedBuffSystem())
                .Add(new JumpBuffSystem())
                .OneFrame<HitComponent>()
                .Inject(gameData);
            fixedUpdateSystems = new EcsSystems(ecsWorld)
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem())
                .Add(new PlayerJumpSystem())
                .Inject(gameData);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(ecsWorld);
#endif

            initSystems.ProcessInjects();
            updateSystems.ProcessInjects();
            fixedUpdateSystems.ProcessInjects();

            initSystems.Init();
            updateSystems.Init();
            fixedUpdateSystems.Init();

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(initSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(updateSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(fixedUpdateSystems);
#endif
        }

        private void Update()
        {
            updateSystems.Run();
        }

        private void FixedUpdate()
        {
            fixedUpdateSystems.Run();
        }

        private void OnDestroy()
        {
            initSystems.Destroy();
            updateSystems.Destroy();
            fixedUpdateSystems.Destroy();
            ecsWorld.Destroy();
        }
    }
}