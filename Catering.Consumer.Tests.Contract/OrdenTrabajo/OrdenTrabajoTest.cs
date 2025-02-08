using Catering.Consumer.Console.Services;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Matchers;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using Xunit.Abstractions;

using System.Threading.Tasks;
using Xunit;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using PactNet.Output.Xunit;
using Catering.Consumer.Console.Models.OrdenTrabajo;

namespace Catering.Consumer.Tests.Contract.OrdenTrabajo
{
    public class OrdenTrabajoTest
    {
        private OrdenTrabajoServices _ordenTrabajoServices;
        private readonly IPactBuilderV4 pactBuilder;
        private readonly int port = 9001;
        private readonly GetOrdenTrabajoByIdDto expectedBody;

        public OrdenTrabajoTest(ITestOutputHelper output)
        {
            _ordenTrabajoServices = new OrdenTrabajoServices(new Uri($"http://localhost:{port}"));

            var pactConfig = new PactConfig
            {
                PactDir = Path.Join("..", "..", "..", "..", "..", "pacts"),
                LogLevel = PactLogLevel.Debug,
                Outputters = new[] { new XunitOutput(output) }
            };

            var pact = Pact.V4("Catering.Consumer", "Catering", pactConfig);

            pactBuilder = pact.WithHttpInteractions(port);

            expectedBody = new GetOrdenTrabajoByIdDto
            {
                Id = Guid.Parse("5E7D8A8F-807B-42F9-BB46-5617D835B881"),
                UsuarioCocineroNombre = "Cocinero",
                RecetaNombre = "Receta",
                Cantidad = 1,
                Estado = "Pendiente",
                Tipo = "Comida"
            };
        }

        [Fact]
        public async Task GetByIdTest()
        {
            pactBuilder
            .UponReceiving("A request to get a catering order")
                .Given("There are a catering order")
                .WithHeader("Accept", "application/json")
                .WithRequest(HttpMethod.Get, "/api/OrdenTrabajo/5e7d8a8f-807b-42f9-bb46-5617d835b881")
            .WillRespond()
                .WithStatus(HttpStatusCode.OK)
                .WithHeader("Content-Type", "application/json; charset=utf-8")
                .WithJsonBody(new TypeMatcher(expectedBody));

            await pactBuilder.VerifyAsync(async ctx =>
            {
                // Act

                _ordenTrabajoServices = new OrdenTrabajoServices(ctx.MockServerUri);
                var response = await _ordenTrabajoServices.GetTransaction(Guid.Parse("5E7D8A8F-807B-42F9-BB46-5617D835B881"));

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            });
        }
    }
}