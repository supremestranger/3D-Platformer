using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Platformer
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private EcsWorld ecsWorld;
        private EcsFilter<PlayerInputComponent> playerFilter;

        public void Run()
        {
            foreach (var i in playerFilter)
            {
                ref var playerInputComponent = ref playerFilter.Get1(i);

                playerInputComponent.moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    var tryJump = ecsWorld.NewEntity();
                    tryJump.Get<TryJump>();
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
}