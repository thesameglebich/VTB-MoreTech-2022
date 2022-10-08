using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Models.CommonModels
{
    public class Result
    {
        /// <summary>
        /// Статус-код. По умолчанию HttpStatusCode.OK
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; } = HttpStatusCode.OK;

        /// <summary>
        /// Список ошибок
        /// </summary>
        public ICollection<Error> Errors { get; } = new List<Error>();

        /// <summary>
        /// Сообщение удачного результата
        /// </summary>
        public string SuccessMessage { get; set; } = string.Empty;

        public Result(HttpStatusCode httpStatusCode, ICollection<Error> errors)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
        }

        public Result(HttpStatusCode httpStatusCode, Error error)
        {
            HttpStatusCode = httpStatusCode;
            Errors = new List<Error>() { error };
        }

        public Result(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }

        protected Result()
        {

        }
    }
}
