using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.Data.Repositories
{
    public interface IComponentRepository
    {
        void Add(Component component);
        Task<Component> GetByIdAsync(int compId);
        bool HasChanges();
        void Remove(Component model);
        Task SaveAsync();
    }
}