using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.UI
{
    public class MainViewerUI : UIPanel
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Debug.Log("escape");
                UIManager.Instance.Show(UIPanelType.MeshGeneratorUI);
            }
        }
    }
}
