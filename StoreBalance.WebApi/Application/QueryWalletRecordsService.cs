using Microsoft.Extensions.Logging;
using StoreBalance.WebApi.Controllers.ApiModel;
using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Domain.Dtos;
using StoreBalance.WebApi.Domain.Repos;
using StoreBalance.WebApi.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Application
{
    public class QueryWalletRecordsService : IQueryWalletRecordsService
    {
        private readonly IWalletRepository _repo;
        private readonly ILogger<QueryWalletRecordsService> _logger;

        public QueryWalletRecordsService(ILogger<QueryWalletRecordsService> logger, IWalletRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<ApplicationResult<IEnumerable<WalletRecordsDto.Response>>> RecordsByPeriod(Guid userId, DateTime beginDate, DateTime endDate)
        {
            var walletRecords = await _repo.RecordsByPeriod(userId, beginDate, endDate);

            if (walletRecords.Any())
            {
                var resp = walletRecords.Select(x => new WalletRecordsDto.Response
                {
                    CreatedAt = x.CreatedAt,
                    CreditAt = x.ApplyAt,
                    RecordType = x.RecordType.ToString(),
                    Value = x.Value                
                }).ToList();

                return ApplicationResult<IEnumerable<WalletRecordsDto.Response>>.Ok(resp);
            }

            return ApplicationResult<IEnumerable<WalletRecordsDto.Response>>
                .NotFound(ApplicationError.New("walletRecords_notfound", "there are no records for this period"));
        }
    }
}
