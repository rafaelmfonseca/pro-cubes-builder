namespace PCB.Core.World.Metadata
{
    public interface IChunkSettings
    {
        int ChunkSizeX { get; }
        int ChunkSizeY { get; }
        int ChunkSizeZ { get; }
        float UVBlockSize { get; }
    }
}
