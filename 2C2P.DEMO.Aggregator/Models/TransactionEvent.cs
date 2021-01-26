using _2C2P.DEMO.Domain.Events;
using MediatR;
using System;

namespace _2C2P.DEMO.Aggregator.Models
{
    public class TransactionEvent : IntegrationEventBase, INotification
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
    }
}
