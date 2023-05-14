using UnityEngine;
using System.IO;
using System.Text;

namespace AbdullahQadeer.Extensions
{
    public static class MeshExtension
    {
        public static void AskToSaveMesh(this Mesh mesh)
        {
            string path = GetSaveFilePath();
            if (string.IsNullOrEmpty(path))
                return;
            mesh.SaveMeshToFile(path);
        }

        private static string GetSaveFilePath()
        {
            string filePath = "";

#if UNITY_EDITOR
            // Handle file saving in the Unity Editor
            filePath = UnityEditor.EditorUtility.SaveFilePanel("Save Mesh", "", "savedMesh.obj", "obj");
#elif UNITY_ANDROID
    // Handle file saving on Android
    AndroidJavaClass environment = new ("android.os.Environment");
    AndroidJavaObject directory = environment.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", environment.GetStatic<string>("DIRECTORY_DOWNLOADS"));
    string path = directory.Call<string>("getAbsolutePath");
    string fileName = "savedMesh.obj";
    filePath = Path.Combine(path, fileName);
#else
            Debug.LogError("File saving not supported in the current platform.");
#endif

            return filePath;
        }


        public static void SaveMeshToFile(this Mesh mesh, string filePath)
        {
            StringBuilder sb = new ();

            // Write the vertex positions
            foreach (Vector3 vertex in mesh.vertices)
            {
                sb.AppendLine(string.Format("v {0} {1} {2}", vertex.x, vertex.y, vertex.z));
            }

            // Write the vertex normals
            foreach (Vector3 normal in mesh.normals)
            {
                sb.AppendLine(string.Format("vn {0} {1} {2}", normal.x, normal.y, normal.z));
            }

            // Write the UV coordinates
            foreach (Vector2 uv in mesh.uv)
            {
                sb.AppendLine(string.Format("vt {0} {1}", uv.x, uv.y));
            }

            // Write the triangles
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                int[] triangles = mesh.GetTriangles(i);
                for (int j = 0; j < triangles.Length; j += 3)
                {
                    int index0 = triangles[j] + 1;
                    int index1 = triangles[j + 1] + 1;
                    int index2 = triangles[j + 2] + 1;

                    sb.AppendLine(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}", index0, index1, index2));
                }
            }

            // Save the mesh data to a file
            File.WriteAllText(filePath, sb.ToString());

            Debug.Log("Mesh saved at: " + filePath);
        }
    }
}