using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Catering.Consumer.Console.Services
{
    public class OrdenTrabajoServices
    {
        private readonly Uri _baseUri;

        public OrdenTrabajoServices(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<HttpResponseMessage> GetTransaction(Guid id)
        {
            using (var client = new HttpClient { BaseAddress = _baseUri })
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var response = await client.GetAsync($"/api/OrdenTrabajo/{id}");
                    return response;
                }
                catch (Exception ex)
                {
                    throw new Exception("There was a problem connecting to Products API.", ex);
                }
            }
        }
    }
}
