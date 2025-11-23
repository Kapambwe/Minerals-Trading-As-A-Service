using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Trading.Management.Services.Mock
{
    public class MockPaymentService : IPaymentService
    {
        private readonly List<Payment> _payments;

        public MockPaymentService()
        {
            _payments = new List<Payment>
            {
                new Payment { Id = Guid.NewGuid().ToString(), TradeId = "a1b2c3d4-e5f6-7890-1234-567890abcdef", Amount = 1500.00m, PaymentDate = DateTime.Now.AddDays(-5), Description = "Variation payment for quantity adjustment" },
                new Payment { Id = Guid.NewGuid().ToString(), TradeId = "a1b2c3d4-e5f6-7890-1234-567890abcdef", Amount = 250.00m, PaymentDate = DateTime.Now.AddDays(-2), Description = "Late delivery penalty" },
                new Payment { Id = Guid.NewGuid().ToString(), TradeId = "b2c3d4e5-f6a7-8901-2345-67890abcdef0", Amount = 500.00m, PaymentDate = DateTime.Now.AddDays(-10), Description = "Quality discrepancy settlement" }
            };
        }

        public Task<IEnumerable<Payment>> GetPaymentsForTradeAsync(string tradeId)
        {
            return Task.FromResult(_payments.Where(p => p.TradeId == tradeId).AsEnumerable());
        }

        public Task<Payment> GetPaymentByIdAsync(string paymentId)
        {
            return Task.FromResult(_payments.FirstOrDefault(p => p.Id == paymentId)!);
        }

        public Task AddPaymentAsync(Payment payment)
        {
            payment.Id = Guid.NewGuid().ToString();
            _payments.Add(payment);
            return Task.CompletedTask;
        }

        public Task UpdatePaymentAsync(Payment payment)
        {
            var existingPayment = _payments.FirstOrDefault(p => p.Id == payment.Id);
            if (existingPayment != null)
            {
                existingPayment.TradeId = payment.TradeId;
                existingPayment.Amount = payment.Amount;
                existingPayment.PaymentDate = payment.PaymentDate;
                existingPayment.Description = payment.Description;
            }
            return Task.CompletedTask;
        }

        public Task DeletePaymentAsync(string paymentId)
        {
            _payments.RemoveAll(p => p.Id == paymentId);
            return Task.CompletedTask;
        }
    }
}

