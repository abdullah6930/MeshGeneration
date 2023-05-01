using AbdullahQadeer.MeshGenerator.Generator;
using System;

namespace AbdullahQadeer.MeshGenerator
{
    public static class MeshGeneratorFactory
    {
        public static BaseMeshGenerator CreateMeshGenerator(MeshGeneratorType meshType, MeshPreset meshPreset, string name = null)
        {
            switch (meshType)
            {
                case MeshGeneratorType.Box:
                    return new BoxMeshGenerator(meshPreset, name);
                case MeshGeneratorType.Quad:
                    return new QuadMeshGenerator(meshPreset, name);
                default:
                    throw new ArgumentException("Invalid mesh type");
            }
        }

        public static BaseMeshGenerator CreateMeshGenerator(MeshGeneratorType meshType, float width, float height, float volume, string name = null)
        {
            switch (meshType)
            {
                case MeshGeneratorType.Box:
                    return new BoxMeshGenerator(width, height, volume, name);
                case MeshGeneratorType.Quad:
                    return new QuadMeshGenerator(width, height, name);
                default:
                    throw new ArgumentException("Invalid mesh type");
            }
        }
    }
}