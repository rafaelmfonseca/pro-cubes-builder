using System;
using UnityEngine;
using System.Collections.Generic;

namespace PCB.Core.Blocks
{
    [Serializable]
    public class BlockRegistry : MonoBehaviour
    {
        private readonly List<Block> _blocks = new List<Block>();

        public Block this[int id]
        {
            get => _blocks[id];
        }

        public void RegistryBlock(int id, Block block)
        {
            _blocks[id] = block;
        }
    }
}
