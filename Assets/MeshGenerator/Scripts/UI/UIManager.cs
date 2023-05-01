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

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        void Start()
        {
            var panelsList = GetComponentsInChildren<UIPanel>(true);
            foreach(var panel in panelsList)
            {
                if (panels.ContainsKey(panel.PanelType))
                    continue;
                panels.Add(panel.PanelType, panel);
            }

            var panelInstance = panels[UIPanelType.MeshGeneratorUI];
            if (!activePanels.Contains(panelInstance))
            {
                activePanels.Add(panelInstance);
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
                        activePanel.SetActive(false);
                        activePanels.RemoveAt(i);
                    }
                }

                if(!activePanels.Contains(panelInstance) )
                {
                    activePanels.Add(panelInstance);
                }

                panelInstance.SetActive(true);
            }
        }

        public void Hide(UIPanelType panelType)
        {
            if (panels.TryGetValue(panelType, out var panelInstance))
            {
                panelInstance.SetActive(false);
                activePanels.Remove(panelInstance);
            }
        }
    }
}