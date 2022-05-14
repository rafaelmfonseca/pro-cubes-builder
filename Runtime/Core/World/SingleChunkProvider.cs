using System;
using UnityEngine;
using PCB.Core.World.Metadata;

namespace PCB.Core.World
{
    [Serializable]
    internal class SingleChunkProvider : MonoBehaviour, IChunkProvider
    {
        public Vector2Int[] ChunksPositions()
        {
            return new[] { new Vector2Int(0, 0) };
        }
    }
}
