namespace InteractiveSeven.Core.Memory
{
    public interface INameAccessor
    {
        string GetCharacterName(string charName);
        void SetCharacterName(string charName, string newName);
    }
}