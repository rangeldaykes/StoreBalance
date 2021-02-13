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
    public class CreateNewWalletCreditFutureService : ICreateNewWalletCreditFutureService
    {
        private readonly IWalletRepository _repo;
        private readonly ILogger<CreateNewWalletCreditFutureService> _logger;
        private readonly IUnitOfWork _uow;

        public CreateNewWalletCreditFutureService(
            ILogger<CreateNewWalletCreditFutureService> logger,
            IWalletRepository repo,
            IUnitOfWork uow)
        {
            _logger = logger;
            _repo = repo;
            _uow = uow;
        }

        public async Task<ApplicationResult<bool>> NewCredit(Guid userId, WalletRecordsDto.ResquestCreditFuture credit)
        {
            var wallet = await _repo.ByUserId(userId);
            if (wallet == Wallet.Vazio)
            {
                return ApplicationResult<bool>
                    .NotFound(ApplicationError.New("wallet_notfound", $"wallet not exist to userId: {userId}"));
            }

            var recordCredit = new WalletRecord(WalletRecordType.Credit, credit.Value, wallet.Id, credit.ApplyAt);

            await _repo.InsertRecord(recordCredit);

            await _uow.SaveChanges();

            return ApplicationResult<bool>.Ok(true);
        }
    }
}
