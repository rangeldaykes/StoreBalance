using System;
using System.ComponentModel.DataAnnotations;

namespace StoreBalance.WebApi.Domain.Dtos
{
    public class WalletRecordsDto
    {
        /// <summary>
        /// Response Wallet Records
        /// </summary>
        public class Response
        {
            public DateTime CreatedAt { get; set; }
            public DateTime CreditAt { get; set; }
            public string RecordType { get; set; }
            public decimal Value { get; set; }
        }

        /// <summary>
        /// Request Debit Wallet
        /// </summary>
        public class ResquestDebit
        {
            [Required(ErrorMessage = "Value is required")]
            public decimal Value { get; set; }

            [Required(ErrorMessage = "Teste is required")]
            public string Teste { get; set; }
        }

        /// <summary>
        /// Request Credit Wallet
        /// </summary>
        public class ResquestCredit
        {
            [Required(ErrorMessage = "Value is required")]
            public decimal Value { get; set; }
        }

        /// <summary>
        /// Request Credit Wallet Future
        /// </summary>
        public class ResquestCreditFuture
        {
            [Required(ErrorMessage = "Value is required")]
            public decimal Value { get; set; }

            [Required(ErrorMessage = "ApplyAt is required")]
            public DateTime ApplyAt { get; set; }
        }
    }
}
