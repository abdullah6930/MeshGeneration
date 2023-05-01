#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using AbdullahQadeer.MeshGenerator.Gizmos;
using AbdullahQadeer.MeshGenerator.Generator;

namespace AbdullahQadeer.MeshGenerator
{
    public class MeshGeneratorTest : MonoBehaviour
    {
        public static MeshGeneratorTest Instance = null;

        [Header("----Mesh To Generate----")]
        [Space]
        public MeshGeneratorType MeshType;

        public float Width = 1;
        public float Height = 1;
        public float Volume = 1;

        private bool showGizmos;

        public bool ShowGizmos
        {
            get { return showGizmos; }
            set
            {
                if (value == showGizmos)
                    return;

                showGizmos = value;
                if (showGizmos)
                    vertexGizmos.Show();
                else
                    vertexGizmos.Hide();
            }
        }

        private VertexGizmos vertexGizmos;

        public BaseMeshGenerator baseMeshGenerator { private set; get; } = null;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void Start()
        {
            baseMeshGenerator?.Dispose();

            baseMeshGenerator = MeshGeneratorFactory.CreateMeshGenerator(MeshType, Width, Height, Volume);

            vertexGizmos?.Clear();

            vertexGizmos = new VertexGizmos(baseMeshGenerator);
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(MeshGeneratorTest))]
    public class QuadGenerationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // for other non-HideInInspector fields
            MeshGeneratorTest script = (MeshGeneratorTest)target;

            // draw checkbox for the bool
            script.ShowGizmos = EditorGUILayout.Toggle("Show Gizmos", script.ShowGizmos);

            if (GUILayout.Button("Generate Mesh"))
            {
                if (!EditorApplication.isPlaying)
                    EditorApplication.isPlaying = true;
                else
                {
                    script.Start();
                }
            }
        }
    }
#endif
}