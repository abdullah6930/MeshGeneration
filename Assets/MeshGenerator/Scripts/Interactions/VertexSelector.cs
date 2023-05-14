using AbdullahQadeer.InputSystem;
using AbdullahQadeer.MeshGenerator.Gizmos;
using UnityEngine;
using EventManager = AbdullahQadeer.Events.EvenManager;

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
        private Vector2 previousInputPosition;

        enum GizmosType
        {
            Default,
            X_Axis,
            Y_Axis,
            Z_Axis
        }

        #region MonoBehaviour Methods
        void Start()
        {
            mainCamera = Camera.main;
            GizmosLayerMask = MeshGeneratorDataLoader.Instance.GizmosLayerMask;
            if(!InputManager.IsInitialized)
            {
                InputManager.Initialze();
            }
        }

        private void OnEnable()
        {
            EventManager.OnTouchInput += HandleTouchInput;
            EventManager.OnMouseInput += HandleMouseInput;

            EventManager.OnTouchInputEnded += HandleTouchInputEnded;
            EventManager.OnMouseInputEnded += HandleMouseInputEnded;
        }

        private void OnDisable()
        {
            EventManager.OnTouchInput -= HandleTouchInput;
            EventManager.OnMouseInput -= HandleMouseInput;

            EventManager.OnTouchInputEnded -= HandleTouchInputEnded;
            EventManager.OnMouseInputEnded -= HandleMouseInputEnded;
        }
        #endregion MonoBehaviour Methods

        #region Input Handling
        private void HandleTouchInput(Vector2 position)
        {
            if (position == previousInputPosition)
            {
                return;
            }

            OnInputReceived(position);
            previousInputPosition = position;
        }

        private void HandleMouseInput(Vector2 position)
        {
            if (position == previousInputPosition)
            {
                return;
            }

            OnInputReceived(position);
            previousInputPosition = position;
        }

        private void HandleTouchInputEnded()
        {
            ResetInput();
        }

        private void HandleMouseInputEnded()
        {
            ResetInput();
        }
        #endregion Input Handling

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

        void OnInputReceived(Vector2 currentPixelCoordinates)
        {
            if (vertexFound && currentVertexIndex != -1 && currentGizmos != null)
            {
                MoveVertex(currentPixelCoordinates);
                return;
            }
            var ray = mainCamera.ScreenPointToRay(currentPixelCoordinates);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, GizmosLayerMask))
            {
                currentVertexGizmos = raycastHit.collider.GetComponentInParent<VertexGizmosMono>();
                if (currentVertexGizmos != null)
                {
                    currentGizmos = raycastHit.collider.gameObject;
                    SetGizmosType();
                    currentGizmosParent = GetParentGizmos(currentGizmos);
                    vertexFound = GetVertex(currentGizmosParent.transform.localPosition, out currentVertexIndex);
                    if (vertexFound)
                    {
                        MoveVertex(currentPixelCoordinates);
                    }
                }
            }
        }

        void ResetInput()
        {
            vertexFound = false;
            currentGizmos = null;
            currentVertexIndex = -1;
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