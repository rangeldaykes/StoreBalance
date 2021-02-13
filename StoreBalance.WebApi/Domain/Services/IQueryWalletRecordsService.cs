using StoreBalance.WebApi.Domain.Common;
using StoreBalance.WebApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreBalance.WebApi.Domain.Services
{
    public interface IQueryWalletRecordsService
    {
        Task<ApplicationResult<IEnumerable<WalletRecordsDto.Response>>> RecordsByPeriod(Guid userId, DateTime beginDate, DateTime endDate);
    }
}
