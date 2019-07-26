using InteractiveSeven.Core.Data;

namespace InteractiveSeven.Core.Diagnostics.Memory
{
    public interface INameAccessor
    {
        string GetCharacterName(CharNames charName);
        void SetCharacterName(CharNames charName, string newName);
    }
}