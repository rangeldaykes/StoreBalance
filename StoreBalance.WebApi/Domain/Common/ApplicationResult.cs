using System.Collections.Generic;

namespace StoreBalance.WebApi.Domain.Common
{
    public class ApplicationResult<T>
    {
        public T Data { get; private set; }
        public bool Succeeded { get; private set; }
        public ResultState State { get; private set; }
        public List<ApplicationError> Errors { get; private set; } = new List<ApplicationError>();

        public static ApplicationResult<T> Ok(T data)
        {
            return new ApplicationResult<T>
            {
                Data = data,
                Succeeded = true,
                State = ResultState.Success
            };
        }

        public static ApplicationResult<T> Error(params ApplicationError[] errors)
        {
            var result = new ApplicationResult<T>
            {
                Data = default,
                Succeeded = false,
                State = ResultState.Error
            };

            result.Errors.AddRange(errors);
            return result;
        }

        public static ApplicationResult<T> NotFound(params ApplicationError[] errors)
        {
            var notFound = new ApplicationResult<T>
            {
                Data = default,
                Succeeded = false,
                State = ResultState.NotFound
            };

            notFound.Errors.AddRange(errors);
            return notFound;
        }
    }

    public enum ResultState
    {
        Success,
        Error,
        NotFound
    }
}
