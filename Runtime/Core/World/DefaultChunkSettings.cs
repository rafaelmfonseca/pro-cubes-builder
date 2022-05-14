using System;
using UnityEngine;
using PCB.Core.World.Metadata;

namespace PCB.Core.World
{
    [Serializable]
    public class DefaultChunkSettings : MonoBehaviour, IChunkSettings
    {
        public byte ChunkSizeX
        {
            get
            {
                return 16;
            }
        }

        public byte ChunkSizeY
        {
            get
            {
                return 255;
            }
        }

        public byte ChunkSizeZ
        {
            get
            {
                return 16;
            }
        }
    }
}
