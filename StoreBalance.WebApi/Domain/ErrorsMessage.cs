
namespace StoreBalance.WebApi.Domain
{
    public static class ErrorsMessage
    {
        public static readonly string
            BalanceNegative = "balance cannot be less than zero",
            UserIdNotValid = "UserId is not valid.",
            ValueNegative = "value cannot be less than zero",
            WalletIdNotValid = "WalletId is not valid.",
            CreditAtInPass = "CreditAt cannot be less then now",
            BalanceLessThanValue = "balance is less than value",
            AppliedAtPass = "AppliedAt cannot be less then now";
    }
}
