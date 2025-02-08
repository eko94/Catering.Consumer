// See https://aka.ms/new-console-template for more information
using Catering.Consumer.Console.Services;

OrdenTrabajoServices services = new(new Uri("http://localhost:5142"));
Console.WriteLine(await services.GetTransaction(Guid.Parse("5E7D8A8F-807B-42F9-BB46-5617D835B881")));
