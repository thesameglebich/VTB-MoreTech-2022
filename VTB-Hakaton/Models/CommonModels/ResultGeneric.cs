using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Models.CommonModels
{
    public class Result<T>: Result where T : new()
    {
        public T Entity { get; }

        public Result(T entity)
        {
            Entity = entity;
        }

        public Result(T entity, string successMessage)
        {
            Entity = entity;
            SuccessMessage = successMessage;
        }

        public Result(HttpStatusCode httpStatusCode, Error error) : base(httpStatusCode, error)
        {
            Entity = new T();
        }
    }
}
