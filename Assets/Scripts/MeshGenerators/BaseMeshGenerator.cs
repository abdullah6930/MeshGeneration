using UnityEngine;

namespace abdullahqadeer.MeshGenerator.Generator
{
    public class BaseMeshGenerator
    {
        public Mesh GeneratedMesh { private set; get; } = null;
        public MeshFilter MeshFilter { private set; get; } = null;

        internal void Init(MeshFilter meshFilter)
        {
            GeneratedMesh = new Mesh();
            MeshFilter = meshFilter;
        }
    }
}