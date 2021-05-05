using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Platformer
{
    public class GameData
    {
        // Cached entity. No need to create filters for it
        public ConfigurationSO configuration;
        public Text coinCounter;
        public GameObject playerWonPanel;
        public GameObject gameOverPanel;
    }
}