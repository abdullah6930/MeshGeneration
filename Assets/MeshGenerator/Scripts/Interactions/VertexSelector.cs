using AbdullahQadeer.MeshGenerator.Gizmos;
using UnityEngine;

namespace AbdullahQadeer.MeshGenerator
{
    public class VertexSelector : MonoBehaviour
    {
        private Camera mainCamera;
        private LayerMask GizmosLayerMask;
        private Vector3[] vertices;
        private bool vertexFound;
        private int currentVertexIndex = -1;
        private GameObject currentGizmos, currentGizmosParent;
        private GizmosType currentGizmosType = GizmosType.Default;
        private VertexGizmosMono currentVertexGizmos;

        enum GizmosType
        {
            Default,
            X_Axis,
            Y_Axis,
            Z_Axis
        }

        void Start()
        {
            mainCamera = Camera.main;
            GizmosLayerMask = MeshGeneratorDataLoader.Instance.GizmosLayerMask;
        }

        bool UpdateVertices()
        {
            if (currentVertexGizmos != null)
            {
                vertices = currentVertexGizmos.GetMeshVertices();
                return true;
            }
            else
            {
                return false;
            }
        }

        void LateUpdate()
        {
            if (Input.GetKey(KeyCode.Mouse0))
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

                    currentVertexGizmos = raycastHit.collider.GetComponentInParent<VertexGizmosMono>();
                    if(currentVertexGizmos == null)
                    {
                        return;
                    }

                    currentGizmos = raycastHit.collider.gameObject;
                    SetGizmosType();
                    currentGizmosParent = GetParentGizmos(currentGizmos);
                    vertexFound = GetVertex(currentGizmosParent.transform.localPosition, out currentVertexIndex);
                }
            }
            else
            {
                vertexFound = false;
                currentGizmos = null;
                currentVertexIndex = -1;
            }

            #region Testing
            //if (checkvertices)
            //{
            //    checkvertices = false;
            //    UpdateVertices();
            //    for (int i = 0; i < vertices.Length; i++)
            //    {
            //        Debug.Log(vertices[i]);
            //    }

            //    var vertexPositionOnScreen = mainCamera.WorldToScreenPoint(vertices[0]);
            //    Debug.Log("vertix position on screen " + vertexPositionOnScreen);
            //    vertexPositionOnScreen = mainCamera.ScreenToWorldPoint(vertexPositionOnScreen);
            //    Debug.Log("vertix position on world " + vertexPositionOnScreen);

            //    Debug.Log("vertex world position " + transform.TransformWorldPoint(vertices[0]));
            //}
            #endregion
        }

        void SetGizmosType()
        {
            if (currentGizmos.CompareTag(MeshGeneratorDataLoader.Instance.DEFAULT_GIZMOS))
            {
                currentGizmosType = GizmosType.Default;
            }
            else if (currentGizmos.CompareTag(MeshGeneratorDataLoader.Instance.X_AXIS_GIZMOS))
            {
                currentGizmosType = GizmosType.X_Axis;
            }
            else if (currentGizmos.CompareTag(MeshGeneratorDataLoader.Instance.Y_AXIS_GIZMOS))
            {
                currentGizmosType = GizmosType.Y_Axis;
            }
            else if (currentGizmos.CompareTag(MeshGeneratorDataLoader.Instance.Z_AXIS_GIZMOS))
            {
                currentGizmosType = GizmosType.Z_Axis;
            }
        }

        GameObject GetParentGizmos(GameObject gizmosGameObject)
        {
            switch (currentGizmosType)
            {
                case GizmosType.Default:
                    return gizmosGameObject;
                default:
                    return gizmosGameObject.transform.parent.gameObject;
            }
        }

        void MoveVertex(Vector2 currentPixelCoordinates)
        {
            if(currentVertexGizmos == null)
            {
                return;
            }
            var worldPoint = currentVertexGizmos.transform.TransformPoint(vertices[currentVertexIndex]);
            var vertexPositionOnScreen = mainCamera.WorldToScreenPoint(worldPoint);
            var worldPointMouse = mainCamera.ScreenToWorldPoint(new Vector3(currentPixelCoordinates.x, currentPixelCoordinates.y, vertexPositionOnScreen.z));

            switch (currentGizmosType)
            {
                case GizmosType.Default:
                    currentGizmosParent.transform.position = worldPointMouse;
                    break;

                case GizmosType.X_Axis:
                    worldPointMouse.y = worldPoint.y;
                    worldPointMouse.z = worldPoint.z;
                    currentGizmosParent.transform.position = worldPointMouse;
                    break;

                case GizmosType.Y_Axis:
                    worldPointMouse.x = worldPoint.x;
                    worldPointMouse.z = worldPoint.z;
                    currentGizmosParent.transform.position = worldPointMouse;
                    break;

                case GizmosType.Z_Axis:
                    worldPointMouse.y = worldPoint.y;
                    worldPointMouse.x = worldPoint.x;
                    currentGizmosParent.transform.position = worldPointMouse;
                    break;
            }

            vertices[currentVertexIndex] = currentGizmosParent.transform.localPosition;
            currentVertexGizmos.SetMeshVertices(vertices);
        }

        bool GetVertex(Vector3 worldPosition, out int vertexIndex)
        {
            if (!UpdateVertices())
            {
                vertexIndex = -1;
                return false;
            }

            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i] == worldPosition)
                {
                    vertexIndex = i;
                    return true;
                }
            }
            vertexIndex = -1;
            return false;
        }
    }
}