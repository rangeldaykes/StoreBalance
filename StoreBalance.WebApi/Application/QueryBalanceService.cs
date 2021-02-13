using Microsoft.Extensions.Logging;
using StoreBalance.WebApi.Domain;
using StoreBalance.WebApi.Domain.Repos;
using StoreBalance.WebApi.Domain.Services;
using StoreBalance.WebApi.Domain.Common;
using System;
using System.Threading.Tasks;
using StoreBalance.WebApi.Domain.Dtos;

namespace StoreBalance.WebApi.Application
{
    public class QueryBalanceService : IQueryBalanceService
    {
        private readonly IWalletRepository _repo;
        private readonly ILogger<QueryBalanceService> _logger;

        public QueryBalanceService(ILogger<QueryBalanceService> logger, IWalletRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<ApplicationResult<WalletDto.Response>> BalanceByUserId(Guid userId)
        {
            var wallet = await _repo.ByUserId(userId);

            if (wallet != Wallet.Vazio)
            {
                return ApplicationResult<WalletDto.Response>.Ok(new WalletDto.Response
                {
                    WalletId = wallet.Id,
                    Balance = wallet.Balance
                });
            }

            return ApplicationResult<WalletDto.Response>
                .NotFound(ApplicationError.New("wallet_notfound", $"wallet not exist to userId: {userId}"));
        }
    }
}
