using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexGizmos
{
    Mesh mesh;
    GameObject gameObject;
    Vector3 sphereSize;
    int gizmosLayer;

    List<GameObject> spheresList = new List<GameObject>();
    List<GameObject> x_AxisList = new List<GameObject>();
    List<GameObject> y_AxisList = new List<GameObject>();

    public VertexGizmos(Mesh mesh, GameObject gameObject, float sphereSize, int gizmosLayer)
    {
        this.mesh = mesh;
        this.gameObject = gameObject;
        this.sphereSize = Vector3.one * sphereSize;
        this.gizmosLayer = gizmosLayer;

        AddVertexSpheres();
    }

    void AddVertexSpheres()
    {
        var vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.tag = DataLoader.Instance.DEFAULT_GIZMOS;
            sphere.layer = gizmosLayer;
            sphere.transform.SetParent(gameObject.transform);
            sphere.transform.localScale = sphereSize;
            sphere.transform.position = vertices[i];

            spheresList.Add(sphere);

            x_AxisList.Add(GameObject.Instantiate(DataLoader.Instance.X_AxisGizmos, sphere.transform));
            y_AxisList.Add(GameObject.Instantiate(DataLoader.Instance.Y_AxisGizmos, sphere.transform));
        }
    }

    public void Show()
    {
        foreach (GameObject sphere in spheresList)
            sphere.SetActive(true);

        foreach (GameObject x_Axis in x_AxisList)
            x_Axis.SetActive(true);

        foreach (GameObject y_Axis in y_AxisList)
            y_Axis.SetActive(true);
    }

    public void Hide()
    {
        foreach (GameObject sphere in spheresList)
            sphere.SetActive(false);

        foreach (GameObject x_Axis in x_AxisList)
            x_Axis.SetActive(false);

        foreach (GameObject y_Axis in y_AxisList)
            y_Axis.SetActive(false);
    }

    public void Clear()
    {
        for (int i = spheresList.Count - 1; i >= 0; i--)
            GameObject.Destroy(spheresList[i]);

        x_AxisList.Clear();
        y_AxisList.Clear();
    }
}
