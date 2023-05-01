using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.UI
{
    public class UIPanel : MonoBehaviour
    {
        public enum UIPanelType
        {
            MainViewerUI,
            MeshGeneratorUI
        }
        public UIPanelType PanelType;
        [HideInInspector] public bool IsShowing = false;

        public void OnEnable()
        {
            IsShowing = true;
        }
        private void OnDisable()
        {
            IsShowing = false;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void Show()
        {
            UIManager.Instance.Show(PanelType);
        }

        public void Hide()
        {
            UIManager.Instance.Hide(PanelType);
        }
    }
}