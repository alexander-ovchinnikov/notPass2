using UnityEditor;
using UnityEngine;
//using WaveEditorView;


namespace Game {
    public class LevelController : MonoBehaviour {

        [ContextMenu("Load in editor")]
        private static void LoadInEditor()
        {

            //EditorWindow.GetWindow(typeof(WaveEditorView));
            //var selected = Selection.activeObject;
            //EditorApplication.OpenSceneAdditive(AssetDatabase.GetAssetPath(selected));
        }

        public class Reward {

        }



        public Reward[] Rewards;
        public bool IsAvailable;
        public bool IsComplete;
        public LevelController[] Previous;
        public WaveModel[] Waves;




        /*
            here have to be the
            button show wave editor
        */

        public void StartLevel () {
            Debug.Log("Start Level");
            for (int i = 0; i< Waves.Length; i++) {
                WaveModel wave = Waves[i];
                WaveController waveController = gameObject.AddComponent<WaveController>();
                if (wave.Previous.Length== 0) {
                    waveController.StartWave(wave);
                } else {
                    for (int k = 0; k < wave.Previous.Length; k++) {
                        //var prev = wave.Previous[k];
                        //prev.
                    }
                }

            }
        }



        // Use this for initialization
        void Start () {
            Debug.Log("STAR");
            StartLevel();

        }

        // Update is called once per frame
        void Update () {

        }
    }
}
