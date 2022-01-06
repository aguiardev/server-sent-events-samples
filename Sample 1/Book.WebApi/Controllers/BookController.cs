using Book.WebApi.Extensions;
using Book.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Book.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sse")]
        public async Task Get(
            [FromServices] IBookService bookService, CancellationToken cancellationToken)
        {
            Response.StatusCode = 200;
            Response.Headers.Append("Cache-Control", "no-cache");
            Response.Headers.Append("Content-Type", "text/event-stream");
            Response.Headers.Append("Connection", "keep-alive");

            async void OnUpdate(object sender, string message)
            {
                if (!Response.HttpContext.IsHttpContextValid())
                    return;
                
                byte[] messageBytes = Encoding.ASCII.GetBytes($"data:{message}\n\n");
                await Response.Body.WriteAsync(messageBytes, 0, messageBytes.Length, cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            };

            bookService.OnUpdate += OnUpdate;

            while (!cancellationToken.IsCancellationRequested)
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

            bookService.OnUpdate -= OnUpdate;

            _logger.LogInformation("Terminou");
        }

        [HttpGet]
        public IActionResult Teste() => Ok("Hello");
    }
}