using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
    private EcsWorld ecsWorld;
    private EcsSystems initSystems;
    private EcsSystems updateSystems;
    private EcsSystems fixedUpdateSystems;
    
    // Cached entities. No need to create filters for them
    public EcsEntity playerEntity { get; set; }
    public EcsEntity cameraEntity { get; set; }

    public ConfigurationSO Configuration;
    public Text coinCounter;
    public GameObject playerWonPanel;
    public GameObject gameOverPanel;

    private void Start()
    {
        ecsWorld = new EcsWorld();

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(ecsWorld);
#endif  

        initSystems = new EcsSystems(ecsWorld)
            .Add(new PlayerInitSystem())
            .Add(new CameraInitSystem())
            .Add(new DangerousInitSystem())
            .Inject(this);
        updateSystems = new EcsSystems(ecsWorld)
            .Add(new PlayerInputSystem())
            .Add(new DangerousRunSystem())
            .Add(new HitSystem())
            .Add(new BuffTimerSystem())
            .OneFrame<HitComponent>()
            .Inject(this);
        fixedUpdateSystems = new EcsSystems(ecsWorld)
            .Add(new PlayerMoveSystem())
            .Add(new CameraFollowSystem())
            .Add(new PlayerJumpSystem())
            .Inject(this);

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
}
