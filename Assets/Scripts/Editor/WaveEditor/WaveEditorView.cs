using Game;
using UnityEditor;
using UnityEngine;

namespace Editor.WaveEditor
{
    public class WaveEditorView : EditorWindow
    {
        public class WaveView : UnityEditor.Editor
        {
            public Rect windowRect = new Rect(100, 100, 200, 100);
            public WaveModel model = null;

            public void Draw(int i)
            {
                EditorGUILayout.BeginHorizontal();
                model = (WaveModel) EditorGUILayout.ObjectField(model, typeof(WaveModel), true);
                GUI.DragWindow();

                EditorGUILayout.EndHorizontal();
            }

            public void OnInspectorGUI()
            {
                DrawDefaultInspector();
            }
        }


        public WaveView wave;
        public LevelController level;
        bool _ = false;

        void OnGUI()
        {
            level = (LevelController) EditorGUILayout.ObjectField("Level: ", level, typeof(LevelController), true);
        }

        /*
        void GetInspector() {
            Type inspectorType = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
            Type trackerType = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.ActiveEditorTracker");

            FieldInfo currentInspectorFieldInfo = inspectorType.GetField("s_CurrentInspectorWindow", BindingFlags.Public | BindingFlags.Static);
            FieldInfo currentActiveTrackerFieldInfo = inspectorType.GetField("m_Tracker", BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo isLockedPropertyInfo = trackerType.GetProperty("isLocked", BindingFlags.Public | BindingFlags.Instance);

            if (currentActiveTrackerFieldInfo != null && currentInspectorFieldInfo != null && isLockedPropertyInfo != null)
            {
                System.Object activeTracker = currentActiveTrackerFieldInfo.GetValue(currentInspectorFieldInfo.GetValue(null));
                bool inspectorIsLocked = (bool)isLockedPropertyInfo.GetValue(activeTracker, null);
                if (inspectorIsLocked)
                {
                    EditorGUILayout.HelpBox("EDITOR IS LOCKED", MessageType.Warning);
                }
            }
        }
        */

        void OnInspectorGui()
        {
            /*
            if (wave.model!=null)
            Editor.CreateEditor(wave.model);
            */
        }

        [MenuItem("Window/WaveEditorView")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(WaveEditorView));
        }

        // Use this for initialization
        void Start()
        {
            wave = new WaveView();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}