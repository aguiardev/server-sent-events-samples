using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Book.WebApi.Services
{
    public class BookHostedService : IHostedService
    {
        private readonly ILogger<IBookService> _logger;
        private readonly IBookService _bookService;

        public BookHostedService(ILogger<IBookService> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço Book iniciado.");

            _ = Task.Run(() => _bookService.GenerateData(cancellationToken), cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço Book parado.");
            return Task.CompletedTask;
        }

    }
}
