using System.Threading.Tasks;
using InteractiveSeven.Core.Models;

namespace InteractiveSeven.Core.Emitters
{
    public interface IMenuHubEmitter // TODO: Move to correct folder/namespace
    {
        Task ShowNewColors(MenuColors menuColors);
    }
}