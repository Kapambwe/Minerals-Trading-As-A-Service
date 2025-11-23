using Platform.Trading.Management.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Trading.Management.Services.Interfaces
{
    public interface IMarginRequestService
    {
        Task<IEnumerable<MarginRequest>> GetMarginRequestsForTradeAsync(string tradeId);
        Task<MarginRequest> GetMarginRequestByIdAsync(string marginRequestId);
        Task AddMarginRequestAsync(MarginRequest marginRequest);
        Task UpdateMarginRequestAsync(MarginRequest marginRequest);
        Task DeleteMarginRequestAsync(string marginRequestId);
    }
}
