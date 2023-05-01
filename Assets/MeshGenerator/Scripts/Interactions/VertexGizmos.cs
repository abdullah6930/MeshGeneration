using AbdullahQadeer.MeshGenerator.Generator;
using System.Collections.Generic;
using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.Gizmos
{
    public class VertexGizmos
    {
        Mesh mesh;
        Transform transform;
        Vector3 sphereSize;

        List<GameObject> gizmosObjects = new List<GameObject>();

        public VertexGizmos(BaseMeshGenerator baseMeshGenerator)
        {
            mesh = baseMeshGenerator.GeneratedMesh;
            transform = baseMeshGenerator.ThisGameObject.transform;
            sphereSize = Vector3.one * MeshGeneratorDataLoader.Instance.SphereSize;

            AddVertexSpheres();
        }

        void AddVertexSpheres()
        {
            var vertices = mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                var gizmosObject = GameObject.Instantiate(MeshGeneratorDataLoader.Instance.Default_AxisGizmos);
                gizmosObject.transform.SetParent(transform);
                gizmosObject.transform.localScale = sphereSize;
                gizmosObject.transform.position = vertices[i];

                gizmosObjects.Add(gizmosObject);
            }
        }

        public void Show()
        {
            foreach (GameObject sphere in gizmosObjects)
                sphere.SetActive(true);
        }

        public void Hide()
        {
            foreach (GameObject sphere in gizmosObjects)
                sphere.SetActive(false);
        }

        public void Clear()
        {
            for (int i = gizmosObjects.Count - 1; i >= 0; i--)
                GameObject.Destroy(gizmosObjects[i]);
        }
    }

}