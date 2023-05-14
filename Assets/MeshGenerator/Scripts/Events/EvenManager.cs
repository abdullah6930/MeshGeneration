using UnityEngine;

namespace AbdullahQadeer.Events
{
    public static class EvenManager
    {
        public delegate void OnGenerateMeshClick();
        public static OnGenerateMeshClick OnGenerateMeshClickEvent;

        public delegate void TouchInputHandler(Vector2 position);
        public static event TouchInputHandler OnTouchInput;

        public delegate void MouseInputHandler(Vector2 position);
        public static event MouseInputHandler OnMouseInput;

        public delegate void TouchInputEndedHandler();
        public static event TouchInputEndedHandler OnTouchInputEnded;

        public delegate void MouseInputEndedHandler();
        public static event MouseInputEndedHandler OnMouseInputEnded;

        public static void HandleTouchInput(Vector2 position)
        {
            OnTouchInput?.Invoke(position);
        }

        public static void HandleMouseInput(Vector2 position)
        {
            OnMouseInput?.Invoke(position);
        }

        public static void HandleTouchInputEnded()
        {
            OnTouchInputEnded?.Invoke();
        }

        public static void HandleMouseInputEnded()
        {
            OnMouseInputEnded?.Invoke();
        }
    }
}
