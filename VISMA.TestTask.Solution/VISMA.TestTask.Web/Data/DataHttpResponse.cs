using System.Net;

namespace VISMA.TestTask.Web.Data
{
    public class DataHttpResponse<T> : DefaultHttpResponse
    {
        public DataHttpResponse(T data, HttpStatusCode statusCode = HttpStatusCode.OK, string message = null)
            : base(statusCode, message)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
