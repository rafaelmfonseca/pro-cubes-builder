using System.Runtime.CompilerServices;

namespace PCB.Core.Utilities
{
    public static class IndexUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetIndex1DFrom3D(int x, int y, int z, int sizeX, int sizeY, int sizeZ)
        {
            return x + sizeX * (y + sizeY * z);
        }
    }
}
