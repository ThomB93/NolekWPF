using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.Data.Repositories
{
    public interface IErrorRepository
    {
        void Add(Error error);
        Task SaveAsync();
    }
}