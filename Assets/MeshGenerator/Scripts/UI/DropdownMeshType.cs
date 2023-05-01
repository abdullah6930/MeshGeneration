using System.Collections.Generic;
using TMPro;

namespace AbdullahQadeer.MeshGenerator.UI
{
    public class DropdownMeshType : TMP_Dropdown
    {
        private Dictionary<int, MeshGeneratorType> meshTypeDictionary = new Dictionary<int, MeshGeneratorType>();

        public void ClearOptions()
        {
            base.ClearOptions();
            meshTypeDictionary.Clear();
        }

        public void AddOption(MeshPreset meshPreset)
        {
            meshTypeDictionary.Add(options.Count, meshPreset.meshType);
            options.Add(new OptionData(meshPreset.meshType.ToString()));
        }

        public MeshGeneratorType GetOption(int index)
        {
            if (meshTypeDictionary.TryGetValue(index, out var meshType))
            {
                return meshType;
            }
            return MeshGeneratorType.Box;
        }
    }
}