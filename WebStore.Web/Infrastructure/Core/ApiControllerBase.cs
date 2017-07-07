using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebStore.Data.Infrastructure;
using WebStore.Model.Models;
using WebStore.Service;

namespace WebStore.Web.Infrastructure.Core
{
    public class ApiControllerBase : ApiController
    {
        private readonly IErrorService errorService;

        public ApiControllerBase(IErrorService errorService)
        {
            this.errorService = errorService;
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;

            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error _error = new Error()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    CreatedDate = DateTime.Now
                };

                errorService.Add(_error);
                errorService.Save();
            }
            catch { }
        }
    }
}
