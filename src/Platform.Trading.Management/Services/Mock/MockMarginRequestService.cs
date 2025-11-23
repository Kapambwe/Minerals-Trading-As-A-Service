using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Trading.Management.Services.Mock
{
    public class MockMarginRequestService : IMarginRequestService
    {
        private readonly List<MarginRequest> _marginRequests;

        public MockMarginRequestService()
        {
            _marginRequests = new List<MarginRequest>
            {
                new MarginRequest { Id = Guid.NewGuid().ToString(), TradeId = "a1b2c3d4-e5f6-7890-1234-567890abcdef", RequestedAmount = 10000.00m, RequestDate = DateTime.Now.AddDays(-10), Status = "Approved" },
                new MarginRequest { Id = Guid.NewGuid().ToString(), TradeId = "a1b2c3d4-e5f6-7890-1234-567890abcdef", RequestedAmount = 2500.00m, RequestDate = DateTime.Now.AddDays(-3), Status = "Pending" },
                new MarginRequest { Id = Guid.NewGuid().ToString(), TradeId = "b2c3d4e5-f6a7-8901-2345-67890abcdef0", RequestedAmount = 5000.00m, RequestDate = DateTime.Now.AddDays(-7), Status = "Rejected" }
            };
        }

        public Task<IEnumerable<MarginRequest>> GetMarginRequestsForTradeAsync(string tradeId)
        {
            return Task.FromResult(_marginRequests.Where(mr => mr.TradeId == tradeId).AsEnumerable());
        }

        public Task<MarginRequest> GetMarginRequestByIdAsync(string marginRequestId)
        {
            return Task.FromResult(_marginRequests.FirstOrDefault(mr => mr.Id == marginRequestId)!);
        }

        public Task AddMarginRequestAsync(MarginRequest marginRequest)
        {
            marginRequest.Id = Guid.NewGuid().ToString();
            _marginRequests.Add(marginRequest);
            return Task.CompletedTask;
        }

        public Task UpdateMarginRequestAsync(MarginRequest marginRequest)
        {
            var existingRequest = _marginRequests.FirstOrDefault(mr => mr.Id == marginRequest.Id);
            if (existingRequest != null)
            {
                existingRequest.TradeId = marginRequest.TradeId;
                existingRequest.RequestedAmount = marginRequest.RequestedAmount;
                existingRequest.RequestDate = marginRequest.RequestDate;
                existingRequest.Status = marginRequest.Status;
            }
            return Task.CompletedTask;
        }

        public Task DeleteMarginRequestAsync(string marginRequestId)
        {
            _marginRequests.RemoveAll(mr => mr.Id == marginRequestId);
            return Task.CompletedTask;
        }
    }
}

