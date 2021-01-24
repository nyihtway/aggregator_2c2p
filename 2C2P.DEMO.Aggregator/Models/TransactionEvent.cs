using _2C2P.DEMO.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2P.DEMO.Aggregator.Models
{
    public class TransactionEvent : IntegrationEventBase, INotification
    {
    }
}
