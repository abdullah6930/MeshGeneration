using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.Generator
{
    public class PyramidMeshGenerator : BaseMeshGenerator
    {
        public PyramidMeshGenerator(MeshPreset meshPreset, string name = null)
        : base(name, meshPreset.width, meshPreset.height, meshPreset.volume)
        {
            Initialize(meshPreset.width, meshPreset.height, meshPreset.volume);
        }

        public PyramidMeshGenerator(float width, float height, float volume, string name = null)
            : base(name, width, height, volume)
        {
            Initialize(width, height, volume);
        }

        protected override void Initialize(float width, float height, float volume)
        {
            Vector3[] vertices = new Vector3[5]
            {
                // Bottom
                new Vector3(width, 0, volume),
                new Vector3(width, 0, 0),
                new Vector3(0, 0, 0),
                new Vector3(0, 0, volume),

                // Top
                new Vector3(width/2, height, volume/2),
            };
            GeneratedMesh.vertices = vertices;

            UpdateMesh();
            UpdateGizmos();
        }

        void UpdateMesh()
        {
            int[] tris = new int[18]
                    {
            //bottom1
            0, 2, 1,
            //bottom2
            0, 3, 2,

            //bottom to top 1
            4, 1, 2,
            //bottom to top 2
            4, 0, 1,

            //bottom to top 3
            4, 3, 0,
            //bottom to top 4
            4, 2, 3
                    };
            GeneratedMesh.triangles = tris;

            //Vector3[] normals = new Vector3[GeneratedMesh.vertexCount];

            //for (int i = 0; i < 4; i++)
            //{
            //    normals[i] = Vector3.forward;
            //}
            //for (int i = 4; i < 8; i++)
            //{
            //    normals[i] = Vector3.back;
            //}

            //GeneratedMesh.SetNormals(normals);

            //Vector2[] uv = new Vector2[8]
            //{
            //    new Vector2(0, 0),
            //    new Vector2(1, 0),
            //    new Vector2(0, 1),
            //    new Vector2(1, 1),

            //    new Vector2(0, 0),
            //    new Vector2(1, 0),
            //    new Vector2(0, 1),
            //    new Vector2(1, 1)
            //};

            //GeneratedMesh.SetUVs(0, uv);

            MeshFilter.mesh = GeneratedMesh;
        }

        public override void UpdateWidth(float value)
        {
            Width = value;
            Initialize(Width, Height, Volume);
        }

        public override void UpdateHeight(float value)
        {
            Height = value;
            Initialize(Width, Height, Volume);
        }

        public override void UpdateVolume(float value)
        {
            Volume = value;
            Initialize(Width, Height, Volume);
        }
    }
}