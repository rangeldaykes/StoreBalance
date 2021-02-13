using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Domain.Services
{
    public interface IQueryFutureBalanceService
    {
        Task<ApplicationResult<WalletDto.Response>> QueryFutureBalance(Guid userId, DateTime endDate);
    }
}
