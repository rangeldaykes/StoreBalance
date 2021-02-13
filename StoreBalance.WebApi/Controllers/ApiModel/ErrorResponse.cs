using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreBalance.WebApi.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace StoreBalance.WebApi.Controllers.ApiModel
{
    public class ErrorResponse
    {
        public IEnumerable<ApplicationError> Errors { get; set; } = new List<ApplicationError>();
    }

    public static class ResponseExtensions
    {
        public static ErrorResponse ErrorResponse<T>(this ApplicationResult<T> result)
        {
            if (result.Errors.Any())
            {
                return new ErrorResponse
                {
                    Errors = result.Errors.Select(e => new ApplicationError
                    {
                        key = e.key,
                        Message = e.Message
                    })
                };
            }
            return new ErrorResponse();
        }
    }

    public static class ModelStateExtensions
    {
        public static ErrorResponse ErrorResponse(this ModelStateDictionary modelState)
        {
            if (modelState != null)
            {
                return new ErrorResponse
                {
                    Errors = modelState.Where(x => x.Value.Errors.Any()).Select(e => new ApplicationError
                    {
                        key = e.Key,
                        Message = string.Join(";", e.Value.Errors.Select(e => e.ErrorMessage))
                    })
                };
            }
            return new ErrorResponse();
        }
    }
}
