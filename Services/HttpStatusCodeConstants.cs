using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services.Enum
{
    public class HttpStatusCodeConstants
    {
        public enum HttpStatusCode
        {
            Ok = 200,
            Created = 201,
            BadRequest = 400,
            Unauthorized = 401,
            NotFound = 404,
            InternalServerError = 500,
            ItemAlreadyInTheBasket = 301,

        }
    }

}