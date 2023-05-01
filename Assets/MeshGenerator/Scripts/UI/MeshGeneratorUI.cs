using AbdullahQadeer.MeshGenerator.Generator;
using UnityEngine;
using UnityEngine.UI;

namespace AbdullahQadeer.MeshGenerator.UI
{
    public class MeshGeneratorUI : UIPanel
    {
        [SerializeField] Button generateMeshButton;

        private BaseMeshGenerator currentMeshGenerator = null;
        private MeshPreset currentMeshPreset = null;

        // Start is called before the first frame update
        void Start()
        {
            generateMeshButton.onClick.AddListener(OnGenerateeMeshClick);
            Initialize();
        }

        private void OnDestroy()
        {
            generateMeshButton.onClick.RemoveAllListeners();
        }

        private void Initialize()
        {
            if (MeshGeneratorDataLoader.Instance.TryGetMeshPreset(MeshGeneratorType.Box, out var meshPreset))
            {
                currentMeshPreset = meshPreset;
                currentMeshGenerator = MeshGeneratorFactory.CreateMeshGenerator(MeshGeneratorType.Box, meshPreset, MeshGeneratorType.Box.ToString());
            }
        }

        private void OnGenerateeMeshClick()
        {
            EvenManager.OnGenerateMeshClickEvent?.Invoke();
        }
    }
}