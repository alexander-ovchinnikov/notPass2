using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game {
    public class WaveController : MonoBehaviour {
        public UnityEvent Complete;
        public UnityEvent EnemyDie;
        public int EnemyDeathCounter;
        private WaveModel wave;

        /*
        public WaveController(WaveModel wave) {
            EnemyCounter = wave.EnemyCount;
            this.wave = wave;
        }
        */

        public void OnComplete() {

        }

        public void OnEnemyDie() {
            EnemyDeathCounter++;
            if (EnemyDeathCounter>=wave.EnemyCount) {
                Complete.Invoke();
                OnComplete();
            }
        }

        public IEnumerator EnemyEmitter() {
            Debug.Log("Enemy emitter init");
            int enemyCounter = wave.EnemyCount;
            while (enemyCounter-- > 0) {

                Debug.Log("Instantiate");
                var enemy = Instantiate(wave.Enemy);
                enemy.transform.position = Vector3.zero;
                enemy.OnDie.AddListener(OnEnemyDie);
                yield return new WaitForSeconds(wave.SpawnInterval);
            }
        }


        public void Init(WaveModel wave) {
            Debug.Log("Init");
            this.wave = wave;
        }

        public void StartWave(WaveModel wave) {

            Debug.Log("Start Level");
            this.wave = wave;
            Init(wave);
            StartCoroutine(EnemyEmitter());
        }
    }
}
