using System.Collections.Concurrent;

namespace PCB.Core
{
    public class ProCubesBuilder
    {
        private readonly ConcurrentBag<ProCubeInstance> _instances = new ConcurrentBag<ProCubeInstance>();

        public void QueueCube(ProCubeInstance instance)
        {
            _instances.Add(instance);
        }

        public void Build()
        {

        }
    }
}
