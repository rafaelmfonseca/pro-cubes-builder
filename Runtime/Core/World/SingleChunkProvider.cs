﻿using System;
using UnityEngine;
using PCB.Core.World.Metadata;

namespace PCB.Core.World
{
    [Serializable]
    internal class SingleChunkProvider : MonoBehaviour, IChunkProvider
    {
        public Vector2Int[] ChunksPositions()
        {
            return new[]
            {
                new Vector2Int(0, 0),
                new Vector2Int(1, 0),
                new Vector2Int(2, 0),
                new Vector2Int(3, 0),
                new Vector2Int(4, 0),
                new Vector2Int(5, 0),

                new Vector2Int(0, 1),
                new Vector2Int(1, 1),
                new Vector2Int(2, 1),
                new Vector2Int(3, 1),
                new Vector2Int(4, 1),
                new Vector2Int(5, 1),

                new Vector2Int(0, 2),
                new Vector2Int(1, 2),
                new Vector2Int(2, 2),
                new Vector2Int(3, 2),
                new Vector2Int(4, 2),
                new Vector2Int(5, 2),
            };
        }
    }
}
