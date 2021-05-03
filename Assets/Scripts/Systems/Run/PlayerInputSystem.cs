using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputSystem : IEcsRunSystem
{
    private Startup startup;
    public void Run()
    {
        if (!startup.playerEntity.IsAlive())
        {
            return;
        }
        ref var playerInputComponent = ref startup.playerEntity.Get<PlayerInputComponent>();
        
        playerInputComponent.moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerInputComponent.jumpInput = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
