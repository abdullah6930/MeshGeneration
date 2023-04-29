#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public static MeshGenerator Instance = null;

    [Header("----Mesh To Generate----")]
    [Space]
    public MeshType MeshType;

    public float Width = 1;
    public float Height = 1;
    public float Volume = 1;

    [Header("----Gizmos-----")]
    [Space]
    public float sphereSize = 0.2f;
    public LayerMask gizmosLayerMask;
    public int layerIndex;

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

    [HideInInspector] public Mesh GeneratedMesh = null;

    GameObject generatedGameObject = null;
    VertexSelector vertexSelector = null;
    MeshFilter meshFilter = null;

    private void Awake()
    {
        if(Instance == null)
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
        InitGameObject();

        if (MeshType == MeshType.Quad)
            _ = new QuadGenerator(ref meshFilter, ref GeneratedMesh, Width, Height);
        else
        {
            _ = new BoxGenerator(ref meshFilter, ref GeneratedMesh, Width, Height, Volume);
        }

        if (vertexGizmos != null)
            vertexGizmos.Clear();

        vertexGizmos = new VertexGizmos(GeneratedMesh, generatedGameObject, sphereSize, layerIndex);

        InitvertexSelector();
    }

    private void InitGameObject()
    {
        var foundObject = GameObject.Find("Generated Object");
        if (foundObject)
            return;

        generatedGameObject = new GameObject("Generated Object");

        MeshRenderer meshRenderer = generatedGameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

        meshFilter = generatedGameObject.AddComponent<MeshFilter>();
    }

    private void InitvertexSelector()
    {
        if (vertexSelector == null)
        {
            vertexSelector = gameObject.AddComponent<VertexSelector>();
        }

        vertexSelector.Initialize(gizmosLayerMask);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(MeshGenerator))]
public class QuadGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields
        MeshGenerator script = (MeshGenerator)target;

        // draw checkbox for the bool
        script.ShowGizmos = EditorGUILayout.Toggle("Show Gizmos", script.ShowGizmos);

        if (GUILayout.Button("Generate Mesh")) 
        {
            if(!EditorApplication.isPlaying)
                EditorApplication.isPlaying = true;
            else
            {
                script.Start();
            }
        }
    }
}
#endif