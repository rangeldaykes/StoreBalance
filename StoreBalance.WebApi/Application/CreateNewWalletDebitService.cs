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
    public class CreateNewWalletDebitService : ICreateNewWalletDebitService
    {
        private readonly IWalletRepository _repo;
        private readonly ILogger<CreateNewWalletDebitService> _logger;
        private readonly IUnitOfWork _uow;

        public CreateNewWalletDebitService(
            ILogger<CreateNewWalletDebitService> logger,
            IWalletRepository repo,
            IUnitOfWork uow)
        {
            _logger = logger;
            _repo = repo;
            _uow = uow;
        }

        public async Task<ApplicationResult<bool>> NewDebit(Guid userId, WalletRecordsDto.ResquestDebit debit)
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

                var recordDebit = new WalletRecord(WalletRecordType.Debit, debit.Value, wallet.Id);
                recordDebit.ApplyRecord(DateTime.Now);

                await _repo.InsertRecord(recordDebit);

                wallet.Debit(debit.Value);

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
