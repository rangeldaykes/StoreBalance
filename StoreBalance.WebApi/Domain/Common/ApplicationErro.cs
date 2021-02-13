
namespace StoreBalance.WebApi.Domain.Common
{
    public class ApplicationError
    {
        public string key { get; set; }
        public string Message { get; set; }

        public ApplicationError() { }

        public static ApplicationError New(string key, string Message)
        {
            return new ApplicationError
            {
                key = key,
                Message = Message
            };
        }
    }
}
