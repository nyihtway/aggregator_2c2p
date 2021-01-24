using _2C2P.DEMO.Domain.Events;
using _2C2P.DEMO.Domain.Models;
using _2C2P.DEMO.Infrastructure.Interfaces;
using AutoMapper;
using MediatR;
using System;

namespace _2C2P.DEMO.Aggregator.Models
{
    public class TransactionEvent : IntegrationEventBase, INotification, IMapTo<Transaction>
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TransactionEvent, Transaction>()
                .ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
        }
    }
}
