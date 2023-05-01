using UnityEngine;

namespace AbdullahQadeer.MeshGenerator.Generator
{
    public class BaseMeshGeneratorMonoComponent : MonoBehaviour
    {
        public BaseMeshGenerator BaseMeshGenerator { get; private set; }

        public void AddBaseMesh(BaseMeshGenerator baseMeshGenerator)
        {
            BaseMeshGenerator = baseMeshGenerator;
        }
    }
}