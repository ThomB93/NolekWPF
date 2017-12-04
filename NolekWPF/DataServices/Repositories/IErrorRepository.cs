using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.DataServices.Repositories
{
    public interface IErrorRepository
    {
        void Add(Error error);
        Task SaveAsync();
    }
}