using UnityEngine;
namespace abdullahqadeer.MeshGenerator.Generator
{
    public class QuadGenerator : BaseMeshGenerator
    {
        public QuadGenerator(MeshFilter meshFilter, float width, float height)
        {
            base.Init(meshFilter);

            Vector3[] vertices = new Vector3[4]
            {
            new Vector3(width, height, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, height, 0)
            };
            GeneratedMesh.vertices = vertices;

            UpdateMesh();
        }

        public void UpdateMesh()
        {
            int[] tris = new int[6]
                    {
            // right triangle
            0, 1, 2,
            // left triangle
            2, 3, 0
                    };
            GeneratedMesh.triangles = tris;

            Vector3[] normals = new Vector3[GeneratedMesh.vertexCount];

            for (int i = 0; i < GeneratedMesh.vertexCount; i++)
            {
                normals[i] = Vector3.forward;
            }
            GeneratedMesh.SetNormals(normals);

            Vector2[] uv = new Vector2[4]
            {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
            };

            GeneratedMesh.SetUVs(0, uv);

            MeshFilter.mesh = GeneratedMesh;
        }
    }
}