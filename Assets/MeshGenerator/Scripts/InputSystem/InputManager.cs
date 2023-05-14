using UnityEngine;
using AbdullahQadeer.Events;

namespace AbdullahQadeer.InputSystem
{
    internal class InputManager : MonoBehaviour
    {
        public static bool IsInitialized { private set; get; }
        private bool isMobilePlatform;

        public static void Initialze()
        {
            if (!IsInitialized)
            {
                GameObject inputManager = new GameObject("InputManager");
                inputManager.AddComponent<InputManager>();
            }
        }

        private void Start()
        {
            // Check if the current platform is Android or iOS
            isMobilePlatform = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
            IsInitialized = true;
        }

        private void Update()
        {
            if (isMobilePlatform)
            {
                HandleTouchInput();
            }
            else
            {
                HandleMouseInput();
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0); // Get the first touch

                // Handle touch input here
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchPosition = touch.position;
                    EvenManager.HandleTouchInput(touchPosition);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    EvenManager.HandleTouchInputEnded();
                }
            }
        }

        private void HandleMouseInput()
        {
            // Handle mouse input here
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition = Input.mousePosition;
                EvenManager.HandleMouseInput(mousePosition);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                EvenManager.HandleMouseInputEnded();
            }
        }
    }
}