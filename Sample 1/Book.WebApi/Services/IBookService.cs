using System;
using System.Threading;
using System.Threading.Tasks;

namespace Book.WebApi.Services
{
    public interface IBookService
    {
        event EventHandler<string> OnUpdate;
        Task GenerateData(CancellationToken cancellationToken);
    }
}
