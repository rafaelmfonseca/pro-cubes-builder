namespace PCB.Core.Blocks.Metadata
{
    public interface IBlockRegistry
    {
        Block this[int id] { get; }
        void RegistryBlock(Block block);
    }
}
