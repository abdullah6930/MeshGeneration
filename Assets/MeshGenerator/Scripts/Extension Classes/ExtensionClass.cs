using UnityEngine;

namespace AbdullahQadeer.Extensions
{
    public static class ExtensionClass
    {
        public static Vector3 TransformWorldPoint(this Transform transform, Vector3 vertex)
        {
            var localToWorld = transform.localToWorldMatrix;

            return localToWorld.MultiplyPoint3x4(vertex);
        }
    }
}