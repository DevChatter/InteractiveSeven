using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.FinalFantasy.Constants;
using InteractiveSeven.Core.FinalFantasy.MemModels;
using InteractiveSeven.Core.Settings;
using System.Linq;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class SceneAccessor : ISceneAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

        private string ProcessName => ApplicationSettings.Instance.ProcessName;

        public SceneAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public Scene GetCurrentScene()
        {
            var buffer = new byte[Addresses.SceneMapStart.NumBytes];
            _memoryAccessor.ReadMem(ProcessName, Addresses.SceneMapStart.Address, buffer);

            string[] enemyNames = new string[3];
            for (int i = 0; i < 3; i++)
            {

                var nameBytes = buffer.Skip(SceneMapConst.ActorSize * i + SceneMapConst.EnemyNameOffset)
                    .TakeWhile(x => x != SceneMapConst.EnemyNameEnd).ToArray();
                enemyNames[i] = nameBytes.MapFf7BytesToString(nameBytes.Length);
            }

            return new Scene(enemyNames);
        }
    }
}