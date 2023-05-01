using AbdullahQadeer.MeshGenerator.Generator;
using System.Collections.Generic;
using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.Gizmos
{
    internal class VertexGizmos
    {
        private Vector3 sphereSize;
        private BaseMeshGenerator baseMeshGenerator;
        
        private List<GameObject> gizmosObjects = new List<GameObject>();

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

                gizmosObjects.Add(gizmosObject);
            }
        }

        public void SetActive(bool value)
        {
            foreach (GameObject sphere in gizmosObjects)
                sphere.SetActive(value);
        }

        private void Clear()
        {
            for (int i = gizmosObjects.Count - 1; i >= 0; i--)
                GameObject.Destroy(gizmosObjects[i]);
        }
    }

}