using AbdullahQadeer.MeshGenerator.Generator;
using UnityEditor;
using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.Gizmos
{
    public class VertexGizmosMono : MonoBehaviour
    {
        #if UNITY_EDITOR
        public bool ShowGizmos = true;
        #endif

        private VertexGizmos vertexGizmos;
        private MeshFilter meshFilter;
        private Mesh mesh;

        void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            if(meshFilter == null || vertexGizmos != null)
                return;
            mesh = meshFilter.mesh;
            vertexGizmos = new VertexGizmos(mesh, transform);
            Show();
        }

        public void AddBaseMesh(BaseMeshGenerator baseMeshGenerator)
        {
            meshFilter = baseMeshGenerator.MeshFilter;
            mesh = baseMeshGenerator.GeneratedMesh;
            vertexGizmos = baseMeshGenerator.vertexGizmos;
        }

        public void Show()
        {
            if(vertexGizmos != null)
                vertexGizmos.SetActive(true);
        }

        public void Hide()
        {
            if (vertexGizmos != null)
                vertexGizmos.SetActive(false);
        }

        public Vector3[] GetMeshVertices()
        {
            if (mesh == null)
                return new Vector3[0];
            return mesh.vertices;
        }

        public void SetMeshVertices(Vector3[] vertices)
        {
            if (mesh == null)
                return;
            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            meshFilter.mesh = mesh;
        }

        public void SetMeshVertex(Vector3 vertex, int index)
        {
            if (mesh == null || mesh.vertices.Length < index)
                return;
            mesh.vertices[index] = vertex;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            meshFilter.mesh = mesh;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(VertexGizmosMono))]
    public class VertexGizmosMonoEditor : Editor
    {
        bool previousGizmosValue = false;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // for other non-HideInInspector fields
            VertexGizmosMono script = (VertexGizmosMono)target;
            if (script == null)
            {
                previousGizmosValue = script.ShowGizmos;
                return;
            }
            if(previousGizmosValue != script.ShowGizmos)
            {
                if(script.ShowGizmos)
                {
                    script.Show();
                }
                else
                {
                    script.Hide();
                }
                previousGizmosValue = script.ShowGizmos;
            }
        }
    }
#endif
}
