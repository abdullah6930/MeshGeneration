using System;
using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.Generator
{
    public abstract class BaseMeshGenerator : IDisposable
    {
        public MeshGeneratorType ThisMeshType { protected set; get; }
        public Mesh GeneratedMesh { private set; get; } = null;
        public MeshFilter MeshFilter { private set; get; } = null;
        public GameObject ThisGameObject { private set; get; }

        public MeshRenderer Renderer { private set; get; }

        public float Width { protected set; get; }
        public float Height { protected set; get; }
        public float Volume { protected set; get; }

        public string Name { private set; get; } = "Generator Mesh";

        private bool disposed = false;

        protected BaseMeshGenerator(string name, float width, float height)
        {
            InitGameObject();
            GeneratedMesh = new Mesh();
            Width = width;
            Height = height;
            if(!string.IsNullOrEmpty(name))
                Name = name;
        }

        protected BaseMeshGenerator(string name, float width, float height, float volume)
        {
            InitGameObject();
            GeneratedMesh = new Mesh();
            Width = width;
            Height = height;
            Volume = volume;
            if(!string.IsNullOrEmpty(name))
                Name = name;
        }

        private void InitGameObject()
        {
            ThisGameObject = new GameObject(Name);

            Renderer = ThisGameObject.AddComponent<MeshRenderer>();
            Renderer.sharedMaterial = new Material(Shader.Find("Standard"));
            MeshFilter = ThisGameObject.AddComponent<MeshFilter>();
        }
        protected abstract void Initialize(float width, float height, float volume = 0);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                }

                // dispose unmanaged resources
                GeneratedMesh.Clear();
                GameObject.Destroy(ThisGameObject);

                disposed = true;
            }
        }

        ~BaseMeshGenerator()
        {
            Dispose(false);
        }
    }
}