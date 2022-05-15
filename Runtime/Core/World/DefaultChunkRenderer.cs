using System;
using UnityEngine;
using PCB.Core.Blocks;
using PCB.Core.Blocks.Metadata;
using PCB.Core.World.Metadata;
using System.Collections.Generic;

namespace PCB.Core.World
{
    [Serializable]
    public class DefaultChunkRenderer : MonoBehaviour, IChunkRenderer
    {
        private readonly static byte[] patternCounterClockwise = new byte[] { 0, 1, 2, 0, 2, 3 };
        private readonly static byte[] patternClockwise = new byte[] { 0, 2, 1, 0, 3, 2 };

        [SerializeField]
        public Material blockMaterial;

        private IChunkSettings _chunkSettings;
        private IBlockRegistry _blockRegistry;

        public void Awake()
        {
            _chunkSettings = GetComponent<IChunkSettings>();
            _blockRegistry = GetComponent<IBlockRegistry>();
        }

        public void Render(Chunk chunk)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<Vector2> uvs = new List<Vector2>();

            for (int x = 0; x < _chunkSettings.ChunkSizeX; x++)
            {
                for (int z = 0; z < _chunkSettings.ChunkSizeZ; z++)
                {
                    for (int y = 0; y < _chunkSettings.ChunkSizeY; y++)
                    {
                        BlockState blockState = chunk[x, y, z];

                        if (blockState.Id == 0) continue;

                        bool renderThisSide = false;

                        /********** // Top face // **********/
                        if (y + 1 < _chunkSettings.ChunkSizeY)
                        {
                            BlockState neighboor = chunk[x, y + 1, z];

                            if (neighboor.Id == 0)
                            {
                                renderThisSide = true;
                            }
                        }
                        else
                        {
                            renderThisSide = true;
                        }

                        if (renderThisSide)
                        {
                            vertices.Add(new Vector3(x, y + 1, z));
                            vertices.Add(new Vector3(x + 1, y + 1, z));
                            vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                            vertices.Add(new Vector3(x, y + 1, z + 1));

                            int vCount = vertices.Count - 4;

                            AddTriangles(triangles, vCount, true);

                            AddUvs(uvs, blockState.Id, CubeSide.Top);

                            renderThisSide = false;
                        }

                        /********** // Bottom face // **********/
                        if (y - 1 >= 0)
                        {
                            BlockState neighboor = chunk[x, y - 1, z];

                            if (neighboor.Id == 0)
                            {
                                renderThisSide = true;
                            }
                        }
                        else
                        {
                            renderThisSide = true;
                        }

                        if (renderThisSide)
                        {
                            vertices.Add(new Vector3(x, y, z));
                            vertices.Add(new Vector3(x + 1, y, z));
                            vertices.Add(new Vector3(x + 1, y, z + 1));
                            vertices.Add(new Vector3(x, y, z + 1));

                            int vCount = vertices.Count - 4;

                            AddTriangles(triangles, vCount, false);

                            AddUvs(uvs, blockState.Id, 1);

                            renderThisSide = false;
                        }

                        /********** // Right face // **********/
                        if (x + 1 < _chunkSettings.ChunkSizeX)
                        {
                            BlockState neighboor = chunk[x + 1, y, z];

                            if (neighboor.Id == 0)
                            {
                                renderThisSide = true;
                            }
                        }
                        else
                        {
                            renderThisSide = true;
                        }
 
                        if (renderThisSide)
                        {
                            vertices.Add(new Vector3(x + 1, y, z));
                            vertices.Add(new Vector3(x + 1, y + 1, z));
                            vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                            vertices.Add(new Vector3(x + 1, y, z + 1));

                            int vCount = vertices.Count - 4;

                            AddTriangles(triangles, vCount, false);

                            AddUvs(uvs, blockState.Id, 2);

                            renderThisSide = false;
                        }

                        /********** // Left face // **********/
                        if (x - 1 > 0)
                        {
                            BlockState neighboor = chunk[x - 1, y, z];

                            if (neighboor.Id == 0)
                            {
                                renderThisSide = true;
                            }
                        }
                        else
                        {
                            renderThisSide = true;
                        }

                        if (renderThisSide)
                        {
                            vertices.Add(new Vector3(x, y, z));
                            vertices.Add(new Vector3(x, y + 1, z));
                            vertices.Add(new Vector3(x, y + 1, z + 1));
                            vertices.Add(new Vector3(x, y, z + 1));

                            int vCount = vertices.Count - 4;

                            AddTriangles(triangles, vCount, true);

                            AddUvs(uvs, blockState.Id, 3);

                            renderThisSide = false;
                        }

                        /********** // Front face // **********/
                        if (z + 1 < 16)
                        {
                            BlockState neighboor = chunk[x , y, z + 1];

                            if (neighboor.Id == 0)
                            {
                                renderThisSide = true;
                            }
                        }
                        else
                        {
                            renderThisSide = true;
                        }

                        if (renderThisSide)
                        {
                            vertices.Add(new Vector3(x, y, z + 1));
                            vertices.Add(new Vector3(x, y + 1, z + 1));
                            vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                            vertices.Add(new Vector3(x + 1, y, z + 1));

                            int vCount = vertices.Count - 4;

                            AddTriangles(triangles, vCount, true);

                            AddUvs(uvs, blockState.Id, 4);

                            renderThisSide = false;
                        }

                        /********** // Back face // **********/
                        if (z - 1 > 0)
                        {
                            BlockState neighboor = chunk[x, y, z - 1];

                            if (neighboor.Id == 0)
                            {
                                renderThisSide = true;
                            }
                        }
                        else
                        {
                            renderThisSide = true;
                        }

                        // Back face
                        if (renderThisSide)
                        {
                            vertices.Add(new Vector3(x, y, z));
                            vertices.Add(new Vector3(x, y + 1, z));
                            vertices.Add(new Vector3(x + 1, y + 1, z));
                            vertices.Add(new Vector3(x + 1, y, z));

                            int vCount = vertices.Count - 4;
                            AddTriangles(triangles, vCount, false);

                            AddUvs(uvs, blockState.Id, 5);
                        }
                    }
                }
            }

            Mesh mesh = chunk.gameObject.GetComponent<MeshFilter>().mesh;
            mesh.Clear();

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs.ToArray();

            mesh.Optimize();
            mesh.RecalculateNormals();

            var renderer = chunk.gameObject.GetComponent<MeshRenderer>();
            renderer.material = blockMaterial;
        }

        private void AddTriangles(List<int> triangles, int vertices, bool clockwise)
        {
            byte[] pattern = (clockwise ? patternClockwise : patternCounterClockwise);

            for (int i = 0; i < pattern.Length; i++)
            {
                triangles.Add(vertices + pattern[i]);
            }
        }

        private void AddUvs(List<Vector2> uvs, int id, int side)
        {
            Block currentBlock = _blockRegistry[id];

            if (currentBlock == null) return;

            Vector2 uv = currentBlock.UVs[side];

            float uvBlockSize = _chunkSettings.UVBlockSize;

            float x = uv.x;
            float y = uv.y;

            x *= uvBlockSize;
            y *= uvBlockSize;

            uvs.Add(new Vector2(x, y));
            uvs.Add(new Vector2(x, y + uvBlockSize));
            uvs.Add(new Vector2(x + uvBlockSize, y + uvBlockSize));
            uvs.Add(new Vector2(x + uvBlockSize, y));
        }
    }
}
