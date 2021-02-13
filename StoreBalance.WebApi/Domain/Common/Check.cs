using System;

namespace StoreBalance.WebApi.Domain.Common
{
    public class Check
    {
        public static void NotEmpty(Guid guidValue, string message)
        {
            if (guidValue == default)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
