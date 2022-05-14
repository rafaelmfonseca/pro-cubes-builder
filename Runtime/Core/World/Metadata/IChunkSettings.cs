namespace PCB.Core.World.Metadata
{
    public interface IChunkSettings
    {
        byte ChunkSizeX { get; }
        byte ChunkSizeY { get; }
        byte ChunkSizeZ { get; }
    }
}
