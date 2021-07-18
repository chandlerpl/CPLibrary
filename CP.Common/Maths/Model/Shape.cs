using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Maths
{
    public static partial class Shape
    {
        public static Model[] BuildCube(int resolution)
        {
            Model[] data = new Model[6];

            for (int side = 0; side < 6; ++side)
            {
                data[side] = new Model
                {
                    //Direction = directionMapping[directions[side]],
                    Vertices = new Vector3F[resolution * resolution],
                    Triangles = new int[(resolution - 1) * (resolution - 1) * 6]
                };

                Vector3F localUp = Vector3F.Up / 2;
                BuildPlane(ref data[side], resolution, localUp, new Vector3F(localUp.Y, localUp.Z, localUp.X));
            }

            return data;
        }

        public static Model[] BuildPlane(int resolution)
        {
            Model[] data = new Model[1];
            data[0] = new Model()
            {
                //Direction = directionMapping[directions[0]]
            };

            BuildPlane(ref data[0], resolution, Vector3F.Up / 2, new Vector3F(Vector3F.Up.Y, Vector3F.Up.Z, Vector3F.Up.X));

            return data;
        }

        private static void BuildPlane(ref Model data, int resolution, Vector3F localUp, Vector3F axisA)
        {
            if (data.Vertices == null)
            {
                data.Vertices = new Vector3F[resolution * resolution];
                data.Triangles = new int[(resolution - 1) * (resolution - 1) * 6];
            }
            int triangle = 0;

            Vector3F axisB = (localUp == Vector3F.Zero ? Vector3F.Up : localUp * 2).Cross(axisA);
            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    int i = (x + y * resolution);
                    Vector2F percent = new Vector2F(x, y) / (resolution - 1);
                    Vector3F pointOnUnitCube = localUp + (percent.X - .5f) * 2 * axisA + (percent.Y - .5f) * 2 * axisB;

                    data.Vertices[i] = pointOnUnitCube;

                    if (x != resolution - 1 && y != resolution - 1)
                    {
                        data.Triangles[triangle++] = i;
                        data.Triangles[triangle++] = i + resolution + 1;
                        data.Triangles[triangle++] = i + resolution;

                        data.Triangles[triangle++] = i;
                        data.Triangles[triangle++] = i + 1;
                        data.Triangles[triangle++] = i + resolution + 1;
                    }
                }
            }
        }

        public static Model[] BuildSphere(int resolution)
        {
            Model[] data = BuildCube(resolution);

            for (int side = 0; side < 6; ++side)
            {
                for (int i = 0; i < data[side].Vertices.Length; ++i)
                {
                    data[side].Vertices[i] = data[side].Vertices[i].Normalized / 2;
                }
            }

            return data;
        }
    }
}
