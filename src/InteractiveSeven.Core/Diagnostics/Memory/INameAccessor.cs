using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Memory
{
    public interface INameAccessor
    {
        string GetCharacterName(CharNames charName);
        void SetCharacterName(CharNames charName, string newName);
    }
}