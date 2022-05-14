using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Concurrent;
using PCB.Core.World.Metadata;
using System.Threading;
using UnityEngine.Pool;

namespace PCB.Core.World
{
    [Serializable]
    public class WorldManager : MonoBehaviour
    {
        [SerializeField]
        public int maxGenerateChunksInFrame = 5;

        public Vector3Int ChunkSize => new Vector3Int(_chunkSettings.ChunkSizeX, _chunkSettings.ChunkSizeY, _chunkSettings.ChunkSizeZ);

        private ConcurrentDictionary<Vector2Int, Chunk> _chunks = new ConcurrentDictionary<Vector2Int, Chunk>();
        private ConcurrentStack<Vector2Int> _generateChunkQueue = new ConcurrentStack<Vector2Int>();
        private IObjectPool<Chunk> _chunksPool;
        private int _numChunksGenerating;

        private IChunkProvider _chunkProvider;
        private IChunkRenderer _chunkRenderer;
        private IChunkSettings _chunkSettings;

        public void Awake()
        {
            _chunksPool = new ObjectPool<Chunk>(
                createFunc: CreatePooledChunk,
                actionOnGet: OnTakeChunkFromPool,
                actionOnRelease: OnChunkReturnedToPool);

            _chunkProvider = GetComponent<IChunkProvider>();
            _chunkRenderer = GetComponent<IChunkRenderer>();
            _chunkSettings = GetComponent<IChunkSettings>();
        }

        public void Update()
        {
            GenerateChunksPositions();
        }

        public void LateUpdate()
        {
            ProcessChunksPositions();
        }

        private void GenerateChunksPositions()
        {
            if (_chunkProvider == null)
                return;

            var positions = _chunkProvider.ChunksPositions();

            for (int i = 0; i < positions.Length; i++)
            {
                var chunkPosition = positions[i];

                if (_generateChunkQueue.Contains(chunkPosition))
                    continue;

                _generateChunkQueue.Push(chunkPosition);
            }
        }

        private void ProcessChunksPositions()
        {
            while (_generateChunkQueue.Count != 0)
            {
                if (_numChunksGenerating >= maxGenerateChunksInFrame)
                    return;

                if (_generateChunkQueue.TryPop(out Vector2Int chunkPosition))
                {
                    StartCoroutine(GenerateChunk(chunkPosition));

                    Interlocked.Increment(ref _numChunksGenerating);
                }
            }
        }

        private IEnumerator GenerateChunk(Vector2Int chunkPosition)
        {
            if (_chunks.ContainsKey(chunkPosition))
                yield break;

            var chunk = _chunksPool.Get();

            _chunks.TryAdd(chunkPosition, chunk);

            chunk.Initialize(chunkPosition, _chunkSettings.ChunkSizeX, _chunkSettings.ChunkSizeY, _chunkSettings.ChunkSizeZ);

            yield return null;
        }

        private Chunk CreatePooledChunk()
        {
            var chunkGameObject = new GameObject();
            chunkGameObject.transform.SetParent(transform);

            return chunkGameObject.AddComponent<Chunk>();
        }

        private void OnTakeChunkFromPool(Chunk chunk)
        {
            chunk.gameObject.SetActive(true);
        }

        private void OnChunkReturnedToPool(Chunk chunk)
        {
            chunk.Dispose();
            chunk.gameObject.SetActive(false);
        }
    }
}
