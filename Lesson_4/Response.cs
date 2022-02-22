using System.Threading.Tasks;
using System.Net.Http;
using System;

namespace Lesson_4
{
    public interface IResponse
    {
        Task<string> GetHtmlBody();
        string Uri { get; set; }
    }

    public class Response : IResponse
    {
        private HttpClient Client;
        private string _Uri;

        public Response() => Client = new HttpClient();

        public string Uri { get { return _Uri; } set { _Uri = value; } }

        public async Task<string> GetHtmlBody()
        {
            try
            {
                return await Client.GetStringAsync(_Uri);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
