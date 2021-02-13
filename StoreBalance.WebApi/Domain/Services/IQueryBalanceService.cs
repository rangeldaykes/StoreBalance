using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Domain.Services
{
    public interface IQueryBalanceService
    {
        Task<ApplicationResult<WalletDto.Response>> BalanceByUserId(Guid userId);
    }
}
