using Bogus;
using Book.WebApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Book.WebApi.Services
{
    public class BookService : IBookService
    {
        public event EventHandler<string> OnUpdate;
        private readonly ILogger<IBookService> _logger;

        public BookService(ILogger<IBookService> logger)
        {
            _logger = logger;
        }
        
        public async Task GenerateData(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"[{DateTime.Now:HH:mm:ss}] GenerateData");

                var ofertas = new Faker<OfertaModel>()
                    .CustomInstantiator(p => new OfertaModel(
                        new CompraModel(
                            p.Random.Number(1, 100),
                            p.Random.Double(1, 99.99),
                            p.Random.Double(15, 50)),
                        new VendaModel(
                            p.Random.Number(1, 100),
                            p.Random.Double(1, 99.99),
                            p.Random.Double(15, 50))))
                    .Generate(50);

                var book = new BookModel(DateTime.Now, ofertas);

                var infoBookSerialized = JsonSerializer.Serialize(book);

                OnUpdate?.Invoke(this, infoBookSerialized);

                await Task.Delay(250, cancellationToken);
            }
        }
    }
}
