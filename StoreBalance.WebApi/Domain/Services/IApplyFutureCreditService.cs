using StoreBalance.WebApi.Domain.Common;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Domain.Services
{
    public interface IApplyFutureCreditService
    {
        Task<ApplicationResult<bool>> Apply();
    }
}
