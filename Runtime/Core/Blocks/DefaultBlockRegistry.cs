using System;
using UnityEngine;
using System.Collections.Generic;
using PCB.Core.Blocks.Metadata;

namespace PCB.Core.Blocks
{
    [Serializable]
    public class DefaultBlockRegistry : MonoBehaviour, IBlockRegistry
    {
        private readonly Block[] _blocks = new Block[byte.MaxValue];

        public Block this[int id]
        {
            get => _blocks[id];
        }

        public void RegistryBlock(Block block)
        {
            _blocks[block.Id] = block;
        }
    }
}
