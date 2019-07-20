namespace InteractiveSeven.Core.Tseng.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MateriaType Type { get; set; }
        public string TypeName => Type.ToString();
    }
}
