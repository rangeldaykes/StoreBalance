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
    public class CreateNewWalletCreditService : ICreateNewWalletCreditService
    {
        private readonly IWalletRepository _repo;
        private readonly ILogger<CreateNewWalletCreditService> _logger;
        private readonly IUnitOfWork _uow;

        public CreateNewWalletCreditService(
            ILogger<CreateNewWalletCreditService> logger,
            IWalletRepository repo,
            IUnitOfWork uow)
        {
            _logger = logger;
            _repo = repo;
            _uow = uow;
        }

        public async Task<ApplicationResult<bool>> NewCredit(Guid userId, WalletRecordsDto.ResquestCredit credit)
        {
            try
            {
                await _uow.BeginTransaction();

                var wallet = await _repo.ByUserId(userId);
                if (wallet == Wallet.Vazio)
                {
                    return ApplicationResult<bool>
                        .NotFound(ApplicationError.New("wallet_notfound", $"wallet not exist to userId: {userId}"));
                }

                var recordCredit = new WalletRecord(WalletRecordType.Credit, credit.Value, wallet.Id);
                recordCredit.ApplyRecord(DateTime.Now);

                await _repo.InsertRecord(recordCredit);

                wallet.Credit(credit.Value);

                await _uow.SaveChanges();

                _uow.CommitTransactionAsync();

                return ApplicationResult<bool>.Ok(true);
            }
            catch (Exception e)
            {
                _uow.RollbackTransaction();
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
