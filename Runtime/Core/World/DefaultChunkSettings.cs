using System;
using UnityEngine;
using PCB.Core.World.Metadata;

namespace PCB.Core.World
{
    [Serializable]
    public class DefaultChunkSettings : MonoBehaviour, IChunkSettings
    {
        public int ChunkSizeX
        {
            get
            {
                return 16;
            }
        }

        public int ChunkSizeY
        {
            get
            {
                return 255;
            }
        }

        public int ChunkSizeZ
        {
            get
            {
                return 16;
            }
        }

        public float UVBlockSize
        {
            get
            {
                return (1f / 5f);
            }
        }
    }
}
