using InteractiveSeven.Core.FinalFantasy.Models;
using System.Collections.Generic;

namespace InteractiveSeven.Core.Data
{
    public interface IGameDatabaseLoader
    {
        (bool loaded, List<Accessory> accessories, List<Armlet> armlets,
            List<Materia> materias, List<Weapon> weapons)
            LoadDataFromKernel(GameDatabase gameDatabase);
    }
}
