
using System;

namespace StoreBalance.WebApi.Domain.Dtos
{
    public class WalletDto
    {
        /// <summary>
        /// Response Wallet
        /// </summary>
        public class Response
        {            
            public Guid WalletId { get; set; }
            public decimal Balance { get; set; }
        }
    }
}
