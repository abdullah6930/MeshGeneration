using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbdullahQadeer.MeshGenerator
{
    public static class EvenManager
    {
        public delegate void OnGenerateMeshClick();
        public static OnGenerateMeshClick OnGenerateMeshClickEvent;
    }
}
