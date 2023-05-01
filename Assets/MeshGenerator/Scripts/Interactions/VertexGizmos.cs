using AbdullahQadeer.MeshGenerator.Generator;
using System.Collections.Generic;
using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.Gizmos
{
    internal class VertexGizmos
    {
        private Vector3 sphereSize;
        private BaseMeshGenerator baseMeshGenerator;
        
        private List<VertexGizmosData> gizmosObjects = new List<VertexGizmosData>();

        private struct VertexGizmosData
        {
            public GameObject gizmosObject;
            public TextMesh textMesh;
            public Collider[] xyzColliders;
            public MeshRenderer[] xyzMeshRenderers;
        }

        public VertexGizmos(BaseMeshGenerator baseMeshGenerator)
        {
            this.baseMeshGenerator = baseMeshGenerator;
            sphereSize = Vector3.one * MeshGeneratorDataLoader.Instance.SphereSize;
        }

        public void UpdateGizmos()
        {
            Clear();
            var vertices = baseMeshGenerator.GeneratedMesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                var gizmosObject = GameObject.Instantiate(MeshGeneratorDataLoader.Instance.Default_AxisGizmos);
                gizmosObject.transform.SetParent(baseMeshGenerator.ThisGameObject.transform);
                gizmosObject.transform.localScale = sphereSize;
                gizmosObject.transform.position = vertices[i];
                
                List<MeshRenderer> renderers = new List<MeshRenderer>();
                renderers.Add(gizmosObject.GetComponent<MeshRenderer>());
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
                    renderer.enabled = value;

                foreach (var collider in vertexGizmosData.xyzColliders)
                    collider.enabled = value;
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