using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace PCB.Core.Jobs
{
    [BurstCompile]
    public struct CubeSideTopJob : IJob
    {
        public NativeArray<Vector3> Vertices;
        public NativeArray<int> Triangles;
        public NativeArray<Color> Colors;
        public NativeArray<Vector2> Uvs;
        public ProCubeInstance CubeInstance;

        public void Execute()
        {
            int index = 0;

            for (int x = 0; x <= CubeInstance.Width; x++)
            {
                for (int y = 0; y <= CubeInstance.Height; y++)
                {
                    Vector3 posBase = new Vector3(x, 0, y);

                    Vertices[index++] = posBase + new Vector3(0, 0, 0);
                    Vertices[index++] = posBase + new Vector3(0, 0, 1);
                    Vertices[index++] = posBase + new Vector3(1, 0, 0);
                    Vertices[index++] = posBase + new Vector3(1, 0, 1);
                }
            }
        }
    }
}
