using System;
using UnityEngine;
using PCB.Core.Blocks;
using PCB.Core.Utilities;

namespace PCB.Core.World
{
    [Serializable]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Chunk : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int _position;

        [SerializeField]
        private BlockState[] _states;

        [SerializeField]
        private byte _chunkSizeX, _chunkSizeY, _chunkSizeZ;

        public ChunkRenderStatus RenderStatus { get; set; }

        public BlockState this[byte x, byte y, byte z]
        {
            get => _states[IndexOf(x, y, z)];
            set => _states[IndexOf(x, y, z)] = value;
        }

        public BlockState this[uint index]
        {
            get => _states[index];
            set => _states[index] = value;
        }

        public Vector2Int Position => _position;
        public Vector2Int WorldPosition => _position * new Vector2Int(_chunkSizeX, _chunkSizeZ);

        public void Initialize(Vector2Int position, byte chunkSizeX, byte chunkSizeY, byte chunkSizeZ)
        {
            gameObject.name = position.ToString();
            gameObject.transform.position = new Vector3(WorldPosition.x, 0, WorldPosition.y);

            _chunkSizeX = chunkSizeX;
            _chunkSizeY = chunkSizeY;
            _chunkSizeZ = chunkSizeZ;

            RenderStatus = ChunkRenderStatus.Pending;
        }

        private int IndexOf(byte x, byte y, byte z) => IndexUtility.GetIndex1DFrom3D(x, y, z, _chunkSizeX, _chunkSizeZ);

        public void Dispose()
        {
            RenderStatus = ChunkRenderStatus.Deleted;
            _states = null;
        }
    }
}
