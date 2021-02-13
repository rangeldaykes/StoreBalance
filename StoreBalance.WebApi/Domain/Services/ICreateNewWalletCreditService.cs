using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Domain.Services
{
    public interface ICreateNewWalletCreditService
    {
        Task<ApplicationResult<bool>> NewCredit(Guid userId, WalletRecordsDto.ResquestCredit credit);
    }
}
