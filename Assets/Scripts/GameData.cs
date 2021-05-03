using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Platformer
{
    public class GameData
    {
        // Cached entities. No need to create filters for them
        public EcsEntity playerEntity { get; set; }
        public EcsEntity cameraEntity { get; set; }

        public ConfigurationSO configuration;
        public Text coinCounter;
        public GameObject playerWonPanel;
        public GameObject gameOverPanel;
    }
}