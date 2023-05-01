using System;
using System.Linq;
using UnityEngine;

namespace AbdullahQadeer.MeshGenerator
{
    //[CreateAssetMenu(fileName = "MeshGeneratorDataLoader", menuName = "ScriptableObjects/MeshGeneratorDataLoader", order = 1)]
    public class MeshGeneratorDataLoader : ScriptableObject
    {
        #region Singleton
        private static MeshGeneratorDataLoader instance = null;
        public static MeshGeneratorDataLoader Instance
        {
            get
            {
                if (instance == null)
                    instance = Resources.Load<MeshGeneratorDataLoader>("DataLoader");
                return instance;
            }
        }
        #endregion

        public GameObject Default_AxisGizmos;

        [Header("TAGS")]
        public string DEFAULT_GIZMOS;
        public string X_AXIS_GIZMOS, Y_AXIS_GIZMOS, Z_AXIS_GIZMOS;
        
        [Space]
        public LayerMask GizmosLayerMask;
        public float SphereSize = 0.2f;

        [Space]
        public MeshPreset[] MeshPresetsList;

        public bool TryGetMeshPreset(MeshGeneratorType meshType, out MeshPreset meshPreset)
        {
            meshPreset = MeshPresetsList.FirstOrDefault(p => p.meshType == meshType);
            return meshPreset != null;
        }
    }

    [Serializable]
    public class MeshPreset
    {
        public MeshGeneratorType meshType;
        public float width, height, volume;
    }
}