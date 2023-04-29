using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator
{
    public TerrainGenerator(ref MeshFilter meshFilter, ref Mesh GeneratedMesh, float width, float height, float volume)
    {
        GeneratedMesh = new Mesh();
        Vector3[] vertices = new Vector3[8]
        {
            // front
            new Vector3(width, height, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, height, 0),

            // back
            new Vector3(width, height, volume),
            new Vector3(width, 0, volume),
            new Vector3(0, 0, volume),
            new Vector3(0, height, volume),

        };
        GeneratedMesh.vertices = vertices;

        UpdateMesh(ref GeneratedMesh, ref meshFilter);
    }

    void UpdateMesh(ref Mesh GeneratedMesh, ref MeshFilter meshFilter)
    {
        int[] tris = new int[36]
                {
            //front right triangle
            0, 1, 2,
            //front left triangle
            2, 3, 0,

            //back right triangle
            6, 5, 4,
            //back left triangle
            4, 7, 6,

            //left side - right triangle
            4, 5, 1,
            //left side - left triangle
            1, 0, 4,

            //right side - right triangle
            3, 2, 6,
            //right side - left triangle
            6, 7, 3,

            //top side - right triangle
            0, 3, 4,
            //top side - left triangle
            3, 7, 4,

            //bottom side - right triangle
            1, 5, 6,
            //bottom side - left triangle
            6, 2, 1
                };
        GeneratedMesh.triangles = tris;

        //Vector3[] normals = new Vector3[GeneratedMesh.vertexCount];

        //for (int i = 0; i < 4; i++)
        //{
        //    normals[i] = Vector3.forward;
        //}
        //for (int i = 4; i < 8; i++)
        //{
        //    normals[i] = Vector3.back;
        //}

        //GeneratedMesh.SetNormals(normals);

        //Vector2[] uv = new Vector2[8]
        //{
        //    new Vector2(0, 0),
        //    new Vector2(1, 0),
        //    new Vector2(0, 1),
        //    new Vector2(1, 1),

        //    new Vector2(0, 0),
        //    new Vector2(1, 0),
        //    new Vector2(0, 1),
        //    new Vector2(1, 1)
        //};

        //GeneratedMesh.SetUVs(0, uv);

        meshFilter.mesh = GeneratedMesh;
    }
}
