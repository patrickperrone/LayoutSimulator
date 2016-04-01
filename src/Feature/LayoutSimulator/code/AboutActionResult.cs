using Sitecore.Feature.LayoutSimulator.Data;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Sitecore.Feature.LayoutSimulator
{
    public class AboutActionResult : IHttpActionResult
    {
        public AboutResult Result { get; private set; }
        public HttpRequestMessage Request { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public AboutActionResult(AboutResult result, HttpRequestMessage request, HttpStatusCode statusCode)
        {
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            Result = result;
            Request = request;
            StatusCode = statusCode;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = StatusCode;
            response.Content = new ObjectContent<AboutResult>(Result, new JsonMediaTypeFormatter());
            response.RequestMessage = Request;
            return response;
        }
    }
}