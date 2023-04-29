using UnityEngine;

public class VertexSelector : MonoBehaviour
{
    Camera mainCamera;
    public LayerMask GizmosLayerMask;

    Vector3[] vertices;

    bool vertexFound = false;
    int currentVertexIndex = -1;
    GameObject currentGizmos, currentGizmosParent;
    GizmosType currentGizmosType = GizmosType.Default;
    public bool checkvertices = false;

    enum GizmosType
    {
        Default,
        X_Axis,
        Y_Axis,
        Z_Axis
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    public void Initialize(LayerMask layerMask)
    {
        GizmosLayerMask = layerMask;
        UpdateVertices();
    }

    void UpdateVertices()
    {
        vertices = MeshGenerator.Instance.GeneratedMesh.vertices;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            var currentPixelCoordinates = Input.mousePosition;

            if (vertexFound && currentVertexIndex != -1 && currentGizmos != null)
            {
                MoveVertex(currentPixelCoordinates);
                return;
            }

            var ray = mainCamera.ScreenPointToRay(currentPixelCoordinates);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, GizmosLayerMask))
            {
                if (raycastHit.collider == null)
                {
                    return;
                }

                currentGizmos = raycastHit.collider.gameObject;
                SetGizmosType();
                currentGizmosParent = GetParentGizmos(currentGizmos);
                currentVertexIndex = GetVertex(currentGizmosParent.transform.localPosition, out vertexFound);
                Debug.Log(currentVertexIndex);
            }
        }
        else
        {
            vertexFound = false;
            currentGizmos = null;
            currentVertexIndex = -1;
        }

        if(checkvertices)
        {
            checkvertices = false;
            UpdateVertices();
            for(int i = 0; i < vertices.Length; i++)
            {
                Debug.Log(vertices[i]);
            }

            var vertexPositionOnScreen = mainCamera.WorldToScreenPoint(vertices[0]);
            Debug.Log("vertix position on screen " + vertexPositionOnScreen);
            vertexPositionOnScreen = mainCamera.ScreenToWorldPoint(vertexPositionOnScreen);
            Debug.Log("vertix position on world " + vertexPositionOnScreen);

            Debug.Log("vertex world position " + transform.TransformWorldPoint(vertices[0]));
        }
    }

    void SetGizmosType()
    {
        if(currentGizmos.CompareTag(DataLoader.Instance.DEFAULT_GIZMOS))
        {
            currentGizmosType = GizmosType.Default;
        }
        else if (currentGizmos.CompareTag(DataLoader.Instance.X_AXIS_GIZMOS))
        {
            currentGizmosType = GizmosType.X_Axis;
        }
        else if (currentGizmos.CompareTag(DataLoader.Instance.Y_AXIS_GIZMOS))
        {
            currentGizmosType = GizmosType.Y_Axis;
        }
        else if (currentGizmos.CompareTag(DataLoader.Instance.Z_AXIS_GIZMOS))
        {
            currentGizmosType = GizmosType.Z_Axis;
        }
    }

    GameObject GetParentGizmos(GameObject gizmosGameObject)
    {
        switch(currentGizmosType)
        {
            case GizmosType.Default:
                return gizmosGameObject;
            default:
                return gizmosGameObject.transform.parent.gameObject;
        }
    }

    void MoveVertex(Vector2 currentPixelCoordinates)
    {
        var worldPoint = transform.TransformWorldPoint(vertices[currentVertexIndex]);
        var vertexPositionOnScreen = mainCamera.WorldToScreenPoint(worldPoint);
        var worldPointMouse = mainCamera.ScreenToWorldPoint(new Vector3(currentPixelCoordinates.x, currentPixelCoordinates.y, vertexPositionOnScreen.z));

        switch (currentGizmosType)
        {
            case GizmosType.Default:
                Debug.Log("change default axis");
                currentGizmosParent.transform.position = worldPointMouse;
                vertices[currentVertexIndex] = currentGizmosParent.transform.localPosition;
                MeshGenerator.Instance.GeneratedMesh.vertices = vertices;
                break;

            case GizmosType.X_Axis:
                Debug.Log("change x axis");
                worldPointMouse.y = worldPoint.y;

                currentGizmosParent.transform.position = worldPointMouse;
                vertices[currentVertexIndex] = currentGizmosParent.transform.localPosition;
                MeshGenerator.Instance.GeneratedMesh.vertices = vertices;
                break;

            case GizmosType.Y_Axis:
                Debug.Log("change y axis");
                worldPointMouse.x = worldPoint.x;

                currentGizmosParent.transform.position = worldPointMouse;
                vertices[currentVertexIndex] = currentGizmosParent.transform.localPosition;
                MeshGenerator.Instance.GeneratedMesh.vertices = vertices;
                break;
        }
    }

    /// <summary>
    /// Get Vertex World Position
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="found"></param>
    /// <returns></returns>
    int GetVertex(Vector3 worldPosition, out bool found)
    {
        UpdateVertices();

        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i] == worldPosition)
            {
                found = true;
                return i;
            }
        }
        found = false;
        return -1;
    }
}
