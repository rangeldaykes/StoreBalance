using Microsoft.Extensions.Logging;
using StoreBalance.WebApi.Domain;
using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Domain.Dtos;
using StoreBalance.WebApi.Domain.Repos;
using StoreBalance.WebApi.Domain.Services;
using System;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Application
{
    public class QueryFutureBalanceService : IQueryFutureBalanceService
    {
        private readonly IWalletRepository _repo;
        private readonly ILogger<QueryFutureBalanceService> _logger;

        public QueryFutureBalanceService(
            ILogger<QueryFutureBalanceService> logger, 
            IWalletRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<ApplicationResult<WalletDto.Response>> QueryFutureBalance(Guid userId, DateTime endDate)
        {
            var wallet = await _repo.ByUserIdWithRecords(userId, endDate);

            if (wallet != Wallet.Vazio)
            {
                var ret = new WalletDto.Response
                {
                    WalletId = wallet.Id,
                    Balance = wallet.Balance
                };

                foreach (var record in wallet.WalletRecords)
                {
                    ret.Balance += record.Value;
                }

                return ApplicationResult<WalletDto.Response>.Ok(ret);
            }

            return ApplicationResult<WalletDto.Response>
                .NotFound(ApplicationError.New("wallet_notfound", $"wallet not exist to userId: {userId}"));
        }
    }
}
