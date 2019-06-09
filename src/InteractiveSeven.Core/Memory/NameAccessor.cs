using InteractiveSeven.Core.Settings;

namespace InteractiveSeven.Core.Memory
{
    public class NameAccessor : INameAccessor
    {
        private readonly IMemoryAccessor _memoryAccessor;

        private string ProcessName => ApplicationSettings.Instance.ProcessName;

        public NameAccessor(IMemoryAccessor memoryAccessor)
        {
            _memoryAccessor = memoryAccessor;
        }

        public string GetCharacterName(string charName)
        {
            CharMemLoc cml = CharMemLoc.ByName(charName);

            byte[] bytes = new byte[cml.Name.NumBytes];
            _memoryAccessor.ReadMem(ProcessName, cml.Name.Address, bytes);

            string characterName = bytes.MapFf7BytesToString();

            return characterName;
        }

        public void SetCharacterName(string charName, string newName)
        {
            CharMemLoc cml = CharMemLoc.ByName(charName);

            byte[] bytes = newName.MapStringToFf7Bytes();

            _memoryAccessor.WriteMem(ProcessName, cml.Name.Address, bytes);
            _memoryAccessor.WriteMem(ProcessName, cml.StartingName.Address, bytes);
        }
    }
}