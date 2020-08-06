using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECorp.UI.Services
{
    public class ValuesService
    {
        IHttpClientFactory clientFactory;
        public ValuesService(IHttpClientFactory _clientFactory)
        {
            clientFactory = _clientFactory;
        }
        public async Task<List<string>>GetValues()
        {
            var httpClient = clientFactory.CreateClient("ServerAPI");
            return await httpClient.GetJsonAsync<List<string>>("/api/values");
        }
    }
}
