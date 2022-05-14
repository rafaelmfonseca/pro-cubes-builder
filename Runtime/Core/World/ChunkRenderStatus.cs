using System;

namespace PCB.Core.World
{
    [Serializable]
    public enum ChunkRenderStatus
    {
        Pending,
        Compiling,
        Compiled,
        Rendering,
        Rendered,
        Done,
        Deleted,
    }
}
