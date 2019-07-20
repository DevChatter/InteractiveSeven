using InteractiveSeven.Core.Models;
using System.Threading.Tasks;

namespace InteractiveSeven.Core.Emitters
{
    public interface IMenuHubEmitter
    {
        Task ShowNewColors(MenuColors menuColors);
    }
}