namespace InteractiveSeven.Core.Data
{
    public class Modded : IModded
    {
        public bool IsLoadedBy7H { get; private set; }

        public void SetLoadedBy7H(bool wasLoaded)
        {
            this.IsLoadedBy7H = wasLoaded;
        }
    }
}