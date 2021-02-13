using Microsoft.Extensions.Logging;
using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Domain.Repos;
using StoreBalance.WebApi.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Application
{
    public class ApplyFutureCreditService : IApplyFutureCreditService
    {
        private readonly IWalletRepository _repo;
        private readonly ILogger<ApplyFutureCreditService> _logger;
        private readonly IUnitOfWork _uow;

        public ApplyFutureCreditService(
            ILogger<ApplyFutureCreditService> logger,
            IWalletRepository repo,
            IUnitOfWork uow)
        {
            _logger = logger;
            _repo = repo;
            _uow = uow;
        }

        public async Task<ApplicationResult<bool>> Apply()
        {
            var credits = await _repo.CreditsToApply();

            _logger.LogInformation($"{credits.Count()} credits to apply");

            try
            {
                await _uow.BeginTransaction();

                foreach (var credit in credits)
                {
                    _logger.LogInformation($"apply creditId: {credit.Id}");

                    credit.ApplyRecord(DateTime.Now);

                    credit.Wallet.Credit(credit.Value);

                    await _uow.SaveChanges();
                }

                _uow.CommitTransactionAsync();
            }
            catch (Exception e)
            {
                _uow.RollbackTransaction();

                _logger.LogError(e, e.Message);

                if (e.InnerException != null)
                    _logger.LogError(e.InnerException, e.InnerException.Message);

                return ApplicationResult<bool>.Error(ApplicationError.New("applyFutureCredit_error", "Error to apply future credit"));
            }

            return ApplicationResult<bool>.Ok(true);
        }
    }
}
