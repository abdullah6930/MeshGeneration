using AbdullahQadeer.MeshGenerator.Gizmos;
using System;
using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.Generator
{
    public abstract class BaseMeshGenerator : IDisposable
    {
        public Mesh GeneratedMesh { private set; get; } = null;
        public MeshFilter MeshFilter { private set; get; } = null;
        public GameObject ThisGameObject { private set; get; }
        public MeshRenderer Renderer { private set; get; }

        public float Width { protected set; get; }
        public float Height { protected set; get; }
        public float Volume { protected set; get; }

        public string Name { private set; get; } = "Generated Mesh";

        private bool disposed = false;

        private VertexGizmos vertexGizmos = null;

        protected BaseMeshGenerator(string name, float width, float height)
        {
            Width = width;
            Height = height;
            if (!string.IsNullOrEmpty(name))
                Name = name;
            InitConstructor();
        }

        protected BaseMeshGenerator(string name, float width, float height, float volume)
        {
            Width = width;
            Height = height;
            Volume = volume;
            if (!string.IsNullOrEmpty(name))
                Name = name;
            InitConstructor();
        }

        private void InitConstructor()
        {
            InitGameObject();
            GeneratedMesh = new Mesh();
            
            vertexGizmos = new VertexGizmos(this);
        }

        private void InitGameObject()
        {
            ThisGameObject = new GameObject(Name);

            Renderer = ThisGameObject.AddComponent<MeshRenderer>();
            Renderer.sharedMaterial = new Material(Shader.Find("Standard"));
            MeshFilter = ThisGameObject.AddComponent<MeshFilter>();
            var baseMeshGeneratorMonoComponent = ThisGameObject.AddComponent<BaseMeshGeneratorMonoComponent>();
            baseMeshGeneratorMonoComponent.AddBaseMesh(this);
        }
        protected abstract void Initialize(float width, float height, float volume = 0);

        public abstract void UpdateWidth(float value);
        public abstract void UpdateHeight(float value);
        public abstract void UpdateVolume(float value);

        #region Gizmos
        protected void UpdateGizmos()
        {
            vertexGizmos.UpdateGizmos();
        }

        public void SetActiveGizmos(bool value)
        {
            vertexGizmos.SetActive(value);
        }

        #endregion Gizmos

        #region Object Dispose
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
        #endregion Object Dispose
    }
}