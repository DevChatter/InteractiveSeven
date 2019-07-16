using System.Threading.Tasks;
using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Emitters
{
    public interface IMenuHubEmitter
    {
        Task ShowNewColors(MenuColors menuColors);
    }
}