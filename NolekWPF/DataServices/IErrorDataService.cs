using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.DataServices
{
    public interface IErrorDataService
    {
        Task AddError(Error error);
    }
}