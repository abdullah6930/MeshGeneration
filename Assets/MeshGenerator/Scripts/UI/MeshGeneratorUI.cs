using AbdullahQadeer.MeshGenerator.Generator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AbdullahQadeer.MeshGenerator.UI
{
    public class MeshGeneratorUI : UIPanel
    {
        [SerializeField] Button selectButton;
        [SerializeField] TMP_InputField widthInputField, heightInputField, volumeInputField;
        [SerializeField] DropdownMeshType meshTypeDropDown;

        private BaseMeshGenerator currentMeshGenerator = null;
        private MeshPreset currentMeshPreset = null;

        void Start()
        {
            selectButton.onClick.AddListener(OnSelectClick);
            widthInputField.onValueChanged.AddListener(OnWidthValueChange);
            heightInputField.onValueChanged.AddListener(OnHeightValueChange);
            volumeInputField.onValueChanged.AddListener(OnVolumeValueChange);
            InitializeDropdown();
        }

        private void OnDestroy()
        {
            selectButton.onClick.RemoveAllListeners();
            widthInputField.onValueChanged.RemoveAllListeners();
            heightInputField.onValueChanged.RemoveAllListeners();
            volumeInputField.onValueChanged.RemoveAllListeners();
        }

        private void GenerateMesh(MeshGeneratorType meshGeneratorType)
        {
            if (MeshGeneratorDataLoader.Instance.TryGetMeshPreset(meshGeneratorType, out var meshPreset))
            {
                currentMeshPreset = meshPreset;
                UpdateInputFieldValues();

                if (currentMeshGenerator != null)
                    currentMeshGenerator.Dispose();
                currentMeshGenerator = MeshGeneratorFactory.CreateMeshGenerator(meshGeneratorType, meshPreset, meshGeneratorType.ToString());
            }
        }

        private void OnSelectClick()
        {
            UIManager.Instance.Show(UIPanelType.MainViewerUI);
        }

        #region Dropdown list
        void InitializeDropdown()
        {
            meshTypeDropDown.ClearOptions();
            foreach(var meshPreset in MeshGeneratorDataLoader.Instance.MeshPresetsList)
            {
                meshTypeDropDown.AddOption(meshPreset);
            }
            meshTypeDropDown.onValueChanged.AddListener(OnMeshTypeDropDownValueChange);
            meshTypeDropDown.SetValueWithoutNotify(1);
            meshTypeDropDown.onValueChanged.Invoke(1);
        }

        void OnMeshTypeDropDownValueChange(int value)
        {
            var meshGeneratorType = meshTypeDropDown.GetOption(value);
            GenerateMesh(meshGeneratorType);
        }
        #endregion Dropdown list

        #region Input Fields
        void UpdateInputFieldValues()
        {
            if (currentMeshPreset == null)
                return;
            widthInputField.text = currentMeshPreset.width.ToString();
            heightInputField.text = currentMeshPreset.height.ToString();
            volumeInputField.text = currentMeshPreset.volume.ToString();
        }

        void OnWidthValueChange(string value)
        {
            if(currentMeshGenerator == null)
            {
                return;
            }
            
            if(float.TryParse(value, out float floatVal))
            {
                currentMeshGenerator.UpdateWidth(floatVal);
            }
        }

        void OnHeightValueChange(string value)
        {
            if (currentMeshGenerator == null)
            {
                return;
            }

            if (float.TryParse(value, out float floatVal))
            {
                currentMeshGenerator.UpdateHeight(floatVal);
            }
        }

        void OnVolumeValueChange(string value)
        {
            if (currentMeshGenerator == null)
            {
                return;
            }

            if (float.TryParse(value, out float floatVal))
            {
                currentMeshGenerator.UpdateVolume(floatVal);
            }
        }
        #endregion Input Fields
    }
}