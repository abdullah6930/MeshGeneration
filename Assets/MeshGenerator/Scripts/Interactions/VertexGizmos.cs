using AbdullahQadeer.MeshGenerator.Generator;
using System.Collections.Generic;
using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.Gizmos
{
    internal class VertexGizmos
    {
        private Vector3 sphereSize;
        private readonly MeshData meshData;
        
        private List<VertexGizmosData> gizmosObjects = new ();

        private struct VertexGizmosData
        {
            public GameObject gizmosObject;
            public TextMesh textMesh;
            public Collider[] xyzColliders;
            public MeshRenderer[] xyzMeshRenderers;
        }

        private class MeshData
        {
            public Mesh Mesh { private set; get; }
            public Transform MeshTransforn { private set; get; }

            public MeshData(Mesh mesh, Transform meshTransform)
            {
                Mesh = mesh;
                MeshTransforn = meshTransform;
            }

            public MeshData(BaseMeshGenerator baseMeshGenerator)
            {
                Mesh = baseMeshGenerator.GeneratedMesh;
                MeshTransforn = baseMeshGenerator.ThisGameObject.transform;
            }
        }


        public VertexGizmos(BaseMeshGenerator baseMeshGenerator)
        {
            meshData = new MeshData(baseMeshGenerator);
            sphereSize = Vector3.one * MeshGeneratorDataLoader.Instance.SphereSize;
        }

        public VertexGizmos(Mesh mesh, Transform meshTransform)
        {
            meshData = new MeshData(mesh, meshTransform);
            sphereSize = Vector3.one * MeshGeneratorDataLoader.Instance.SphereSize;
            UpdateGizmos();
        }

        public void UpdateGizmos()
        {
            Clear();
            var vertices = meshData.Mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                var gizmosObject = GameObject.Instantiate(MeshGeneratorDataLoader.Instance.Default_AxisGizmos);
                gizmosObject.transform.SetParent(meshData.MeshTransforn);
                gizmosObject.transform.localScale = sphereSize;
                gizmosObject.transform.localPosition = vertices[i];

                List<MeshRenderer> renderers = new()
                {
                    gizmosObject.GetComponent<MeshRenderer>()
                };
                for (int j = 0; j < 3; j++)
                {
                    renderers.Add(gizmosObject.transform.GetChild(j).GetComponent<MeshRenderer>());
                }

                VertexGizmosData vertexGizmosData = new VertexGizmosData
                {
                    gizmosObject = gizmosObject,
                    textMesh = gizmosObject.GetComponentInChildren<TextMesh>(),
                    xyzColliders = gizmosObject.GetComponentsInChildren<Collider>(),
                    xyzMeshRenderers = renderers.ToArray()
                };

                if(vertexGizmosData.textMesh != null)
                {
                    vertexGizmosData.textMesh.text = i.ToString();
                    vertexGizmosData.textMesh.gameObject.SetActive(false);
                }

                gizmosObjects.Add(vertexGizmosData);
            }
        }

        public void SetActive(bool value)
        {
            foreach (var vertexGizmosData in gizmosObjects)
            {
                foreach (var renderer in vertexGizmosData.xyzMeshRenderers)
                {
                    if (renderer)
                        renderer.enabled = value;
                }
                foreach (var collider in vertexGizmosData.xyzColliders)
                {
                    if(collider)
                        collider.enabled = value;
                }
            }
        }

        public void SetActiveVertexIndices(bool value)
        {
            foreach (var vertexGizmosData in gizmosObjects)
                vertexGizmosData.textMesh.gameObject.SetActive(value);
        }

        private void Clear()
        {
            for (int i = gizmosObjects.Count - 1; i >= 0; i--)
                GameObject.Destroy(gizmosObjects[i].gizmosObject);

            gizmosObjects.Clear();
        }
    }

}