using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Domain.Services
{
    public interface ICreateNewWalletDebitService
    {
        Task<ApplicationResult<bool>> NewDebit(Guid userId, WalletRecordsDto.ResquestDebit debit);
    }
}
