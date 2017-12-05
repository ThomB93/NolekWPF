using System.Threading.Tasks;
using NolekWPF.Model;

namespace NolekWPF.Data.DataServices
{
    public interface IErrorDataService
    {
        Task AddError(Error error);
    }
}