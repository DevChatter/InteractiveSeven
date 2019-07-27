using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.FinalFantasy;
using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public class NameAccessor : INameAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;
        private readonly IGameMomentAccessor _momentAccessor;

        private string ProcessName => ApplicationSettings.Instance.ProcessName;

        public NameAccessor(IMemoryAccessor memoryAccessor, IGameMomentAccessor momentAccessor)
        {
            _memoryAccessor = memoryAccessor;
            _momentAccessor = momentAccessor;
        }

        public string GetCharacterName(CharNames charName)
        {
            CharMemLoc cml = CharMemLoc.ByName(charName);

            byte[] bytes = new byte[cml.Name.NumBytes];
            _memoryAccessor.ReadMem(ProcessName, cml.Name.Address, bytes);

            string characterName = bytes.MapFf7BytesToString();

            return characterName;
        }

        public void SetCharacterName(CharNames charName, string newName)
        {
            CharMemLoc cml = CharMemLoc.ByName(charName);

            byte[] bytes = newName.MapStringToFf7Bytes();

            if (_momentAccessor.AtMomentOrLater(charName.AllowNamingAfter))
            {
                _memoryAccessor.WriteMem(ProcessName, cml.Name.Address, bytes);
            }
            _memoryAccessor.WriteMem(ProcessName, cml.StartingName.Address, bytes);
        }
    }
}