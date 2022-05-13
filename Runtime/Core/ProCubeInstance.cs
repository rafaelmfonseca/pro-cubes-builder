namespace PCB.Core
{
    public struct ProCubeInstance
    {
        private readonly CubeSide _side;
        private readonly int _width;
        private readonly int _height;

        public CubeSide Side => _side;
        public int Width => _width;
        public int Height => _height;

        public ProCubeInstance(CubeSide side, int width, int height)
        {
            _side = side;
            _width = width;
            _height = height;
        }
    }
}
