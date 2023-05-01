using System.Collections.Generic;
using UnityEngine;
using static AbdullahQadeer.MeshGenerator.UI.UIPanel;

namespace AbdullahQadeer.MeshGenerator.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        private Dictionary<UIPanelType, UIPanel> panels = new Dictionary<UIPanelType, UIPanel>();
        private List<UIPanel> activePanels = new List<UIPanel>();

        // Start is called before the first frame update
        void Start()
        {
            var panelsList = GetComponentsInChildren<UIPanel>(true);
            foreach(var panel in panelsList)
            {
                if (panels.ContainsKey(panel.PanelType))
                    continue;
                panels.Add(panel.PanelType, panel);
            }
        }

        public void Show(UIPanelType panelType)
        {
            if(panels.TryGetValue(panelType, out var panelInstance) )
            {
                if (panelInstance.IsShowing)
                    return;

                for(int  i = activePanels.Count - 1; i >= 0; i--)
                {
                    var activePanel = activePanels[i];
                    if (activePanel != null && activePanel.PanelType != panelType)
                    {
                        activePanel.enabled = false;
                        activePanels.RemoveAt(i);
                    }
                }

                if(!activePanels.Contains(panelInstance) )
                {
                    activePanels.Add(panelInstance);
                }

                panelInstance.enabled = true;
            }
        }

        public void Hide(UIPanelType panelType)
        {
            if (panels.TryGetValue(panelType, out var panelInstance))
            {
                panelInstance.enabled = false;
                activePanels.Remove(panelInstance);
            }
        }
    }
}