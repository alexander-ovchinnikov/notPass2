using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class WaveModel : MonoBehaviour {
        public int TimeOffset;
        public WaveModel[] Previous;
        public Enemy Enemy; /* ISpawnable */
        public int SpawnInterval;
        public int EnemyCount;
    }
}
