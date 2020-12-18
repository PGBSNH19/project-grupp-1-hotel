using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Services.Communication
{
    public class ServiceResponse<T> where T : class
    {
        public T Entity { get; private set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public ServiceResponse(bool success, string message, T entity)
        {
            this.Entity = entity;
            Success = success;
            Message = message;
        }

        /// <summary>
        /// Creates a success response
        /// </summary>
        /// <param name="entity"></param>
        public ServiceResponse(T entity) : this(true, string.Empty, entity)
        { }

        /// <summary>
        /// Creates an error response
        /// </summary>
        /// <param name="message"></param>
        public ServiceResponse(string message) : this(false, message, null)
        { }
    }
}
