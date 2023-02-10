using System.Collections.Generic;
using InteractiveSeven.Core.FinalFantasy.Models;

namespace InteractiveSeven.Core.Data
{
    public interface IGameDatabaseLoader
    {
        (bool loaded, List<Accessory> accessories, List<Armlet> armlets,
            List<Materia> materias, List<Weapon> weapons)
            LoadDataFromKernel(GameDatabase gameDatabase);
    }
}
