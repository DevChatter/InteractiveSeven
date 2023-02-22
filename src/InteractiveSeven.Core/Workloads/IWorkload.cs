using System.Threading.Tasks;

namespace InteractiveSeven.Core.Workloads
{
    public interface IWorkload
    {
        Task Run();
    }
}
