using System.Runtime.CompilerServices;

namespace PCB.Core.Utilities
{
    public static class IndexUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetIndex1DFrom3D(byte x, byte y, byte z, byte sizeX, byte sizeZ)
        {
            return x + sizeX * (z + y * sizeZ);
        }
    }
}
