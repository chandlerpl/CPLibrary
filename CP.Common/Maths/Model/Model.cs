using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Maths
{
    public class Model
    {
        public string name;

        private List<Model> subModels = new List<Model>();

        private Vector3F[] vertices;
        private int[] triangles;
        private Vector3F[] normals;

        public Vector3F[] Vertices { get => vertices; set { vertices = value; normals = null; } }
        public int[] Triangles { get => triangles; set { triangles = value; normals = null; } }
        public Vector3F[] Normals { get { if (normals == null) CalculateNormals(); return normals; } private set => normals = value; }

        private void CalculateNormals()
        {
            normals = new Vector3F[vertices.Length];

            for (int i = 0; i < triangles.Length; i += 3)
            {
                Vector3F one = vertices[triangles[i]];
                Vector3F two = vertices[triangles[i + 1]];
                Vector3F three = vertices[triangles[i + 2]];

                Vector3F n1 = (two - one).Cross(three - one).Normalized;

                normals[triangles[i]] += n1;
                normals[triangles[i + 1]] += n1;
                normals[triangles[i + 2]] += n1;
            }

            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = normals[i].Normalized;
            }
        }
    }
}
