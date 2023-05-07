#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using AbdullahQadeer.MeshGenerator.Generator;
using AbdullahQadeer.Extensions;

namespace AbdullahQadeer.MeshGenerator
{
    public class MeshGeneratorTest : MonoBehaviour
    {
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
                if(value)
                {
                    ShowVertexIndices = false;
                }
                if (value == showGizmos)
                    return;

                showGizmos = value;
                baseMeshGenerator?.SetActiveGizmos(showGizmos);
            }
        }

        private bool showVertexIndices;
        public bool ShowVertexIndices
        {
            get { return showVertexIndices; }
            set
            {
                if (value)
                {
                    ShowGizmos = false;
                }
                if (value == showVertexIndices)
                    return;
                showVertexIndices = value;
                baseMeshGenerator?.SetActiveVertexIndices(showVertexIndices);
            }
        }

        public BaseMeshGenerator baseMeshGenerator { private set; get; } = null;

        public void Start()
        {
            baseMeshGenerator?.Dispose();

            baseMeshGenerator = MeshGeneratorFactory.CreateMeshGenerator(MeshType, Width, Height, Volume);

            ShowGizmos = true;
        }

        public void SaveMesh()
        {
            if (baseMeshGenerator == null)
            {
                Debug.LogError("baseMeshGenerator not Found");
                return;
            }
            if (baseMeshGenerator.GeneratedMesh == null)
            {
                Debug.LogError("baseMeshGenerator.GeneratedMesh not Found");
                return;
            }
            baseMeshGenerator.GeneratedMesh.AskToSaveMesh();
        }

#if UNITY_EDITOR
        private float previousWidth, previousHeight, previousVolume;

        private void OnValidate()
        {
            if (Width != previousWidth)
            {
                previousWidth = Width;
                baseMeshGenerator?.UpdateWidth(Width);
            }
            if (Height != previousHeight)
            {
                previousHeight = Height;
                baseMeshGenerator?.UpdateHeight(Height);
            }
            if (Volume != previousVolume)
            {
                previousVolume = Volume;
                baseMeshGenerator?.UpdateVolume(Volume);
            }
        }
#endif
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(MeshGeneratorTest))]
    public class MeshGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // for other non-HideInInspector fields
            MeshGeneratorTest script = (MeshGeneratorTest)target;

            // draw checkbox for the bool
            script.ShowGizmos = EditorGUILayout.Toggle("Show Gizmos", script.ShowGizmos);

            script.ShowVertexIndices = EditorGUILayout.Toggle("Show Vertex Indices", script.ShowVertexIndices);

            if (GUILayout.Button("Generate Mesh"))
            {
                if (!EditorApplication.isPlaying)
                    EditorApplication.isPlaying = true;
                else
                {
                    script.Start();
                }
            }

            if (EditorApplication.isPlaying && GUILayout.Button("Save Mesh"))
            {
                script.SaveMesh();
            }
        }
    }
#endif
}