using AbdullahQadeer.Extensions;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CombineMeshes : MonoBehaviour
{
    void Start()
    {
        // Get all the child mesh filters
        MeshFilter[] childMeshFilters = GetComponentsInChildren<MeshFilter>();

        // Create a new combined mesh
        Mesh combinedMesh = new Mesh();

        // Collect all the separate meshes
        CombineInstance[] combineInstances = new CombineInstance[childMeshFilters.Length];

        for (int i = 0; i < childMeshFilters.Length; i++)
        {
            combineInstances[i].mesh = childMeshFilters[i].sharedMesh;
            combineInstances[i].transform = childMeshFilters[i].transform.localToWorldMatrix;

            // Optionally, disable the child mesh renderers
            childMeshFilters[i].gameObject.SetActive(false);
        }

        // Combine the meshes
        combinedMesh.CombineMeshes(combineInstances, true, true);

        // Set the combined mesh to the parent mesh filter
        GetComponent<MeshFilter>().sharedMesh = combinedMesh;

        // Optionally, enable the parent mesh renderer
        GetComponent<MeshRenderer>().enabled = true;

        combinedMesh.AskToSaveMesh();
    }
}
