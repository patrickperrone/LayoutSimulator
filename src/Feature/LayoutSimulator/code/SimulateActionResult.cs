using Sitecore.Feature.LayoutSimulator.Data;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Net.Http.Headers;

namespace Sitecore.Feature.LayoutSimulator
{
    public class SimulateActionResult : IHttpActionResult
    {
        public SimulateResult Result { get; private set; }
        public HttpRequestMessage Request { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }

        public SimulateActionResult(SimulateResult result, HttpRequestMessage request, HttpStatusCode statusCode)
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

            if (Result.Success)
            {
                response.Content = new StringContent(Result.SimulatedHtml);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            }
            else
            {
                response.Content = new ObjectContent<SimulateResult>(Result, new JsonMediaTypeFormatter());
            }

            response.RequestMessage = Request;
            return response;
        }
    }
}