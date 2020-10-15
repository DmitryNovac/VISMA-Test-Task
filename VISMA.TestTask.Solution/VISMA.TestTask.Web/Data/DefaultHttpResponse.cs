using System.Net;

namespace VISMA.TestTask.Web.Data
{
    public class DefaultHttpResponse
    {
        public DefaultHttpResponse(HttpStatusCode statusCode = HttpStatusCode.OK, string message = null)
        {
            StatusCode = statusCode;
            ResponseMessage = message;
        }

        public HttpStatusCode StatusCode { get; set; }

        public string ResponseMessage { get; set; }
    }
}
