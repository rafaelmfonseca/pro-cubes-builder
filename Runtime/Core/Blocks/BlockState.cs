using System;

namespace PCB.Core.Blocks
{
    [Serializable]
    public struct BlockState
    {
        private readonly int _id;

        public int Id => _id;

        public BlockState(int id)
        {
            _id = id;
        }
    }
}
