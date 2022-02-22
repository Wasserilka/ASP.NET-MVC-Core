using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Lesson_4
{
    internal sealed class FacadeClass
    {
        private readonly HttpClient client = new HttpClient();

        internal async Task<string> GetHttp()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://yandex.ru/pogoda/?lat=55.753215&lon=37.622504");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                return e.Message;
            }
        }
    }
}
