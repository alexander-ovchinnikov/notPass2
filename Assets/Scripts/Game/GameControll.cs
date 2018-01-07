using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameControll : MonoBehaviour
    {
        [SerializeField] private LevelNode _currentLevel;
        public static GameControll Instance { get; private set; }

        public LevelNode CurrentLevel
        {
            get { return _currentLevel; }
            set { _currentLevel = value; }
        }


        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }

        //todo:: move somewhere else

        private int _activeWavesCount = 0;
        public Action<LevelNode> OnLevelComplete;

        public int ActiveWavesCount
        {
            get { return _activeWavesCount; }
            set
            {
                if (value == 0 && _activeWavesCount != 0)
                {
                    if (OnLevelComplete != null) OnLevelComplete.Invoke(_currentLevel);
                }

                _activeWavesCount = value;
            }
        }

        private void LevelComplete(LevelNode node)
        {
            node.Complete();
        }

        public void StartLevel()
        {
            string scene = "Scenes/Game";
            SceneManager.LoadScene(scene);

            LevelNode level = CurrentLevel;
            List<WaveNode> waveNodes = level.getInner();
            foreach (WaveNode waveNode in waveNodes)
            {
                waveNode.IsRunning = false;
                if (waveNode.IsReady())
                {
                    RunWaveNode(waveNode);
                }
            }
        }

        private void RunWaveNode(WaveNode node)
        {
            ActiveWavesCount++;
            if (node.IsRunning)
            {
                return;
            }

            node.IsRunning = true;
            StartCoroutine(BeginWaveSpawn(node));
            node.OnComplete += OnWaveComplete;
        }

        private void OnWaveComplete(WaveNode node)
        {
            foreach (WaveNode childNode in node.getNext())
            {
                RunWaveNode(childNode);
            }

            node.OnComplete -= OnWaveComplete;
            ActiveWavesCount--;
        }

        private IEnumerator BeginWaveSpawn(WaveNode node)
        {
            while (!node.IsReady())
            {
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(node.Delay);
            int spawnedEnemiesCount = 0; // node.EnemiesCount;
            List<Enemy> enemies = new List<Enemy>();
            while (spawnedEnemiesCount < node.EnemiesCount)
            {
                GameObject enemyGO = Instantiate(node.Enemy);
                spawnedEnemiesCount++;
                Enemy enemy = enemyGO.GetComponent<Enemy>();
                enemies.Add(enemy);
                enemy.OnDie.AddListener(
                    () => { enemies.Remove(enemy); }
                );
                yield return new WaitForSeconds(node.SpawnInterval);
            }

            while (enemies.Count > 0)
            {
                yield return new WaitForSeconds(1f);
            }

            node.Complete();
        }


        // Use this for initialization
        void Start()
        {
            OnLevelComplete += LevelComplete;
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}