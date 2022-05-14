using UnityEngine;

namespace PCB.Core.World.Metadata
{
    public interface IChunkProvider
    {
        Vector2Int[] ChunksPositions();
    }
}
