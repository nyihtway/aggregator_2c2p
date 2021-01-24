using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using _2C2P.DEMO.Domain.Events;

namespace _2C2P.DEMO.AGGREGATOR.BackgroundServices
{
    public class TransactionSubscriberService : BackgroundService
    {
        private readonly IBusClient _busClient;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediatR;

        public TransactionSubscriberService(IBusClient busClient, IConfiguration configuration, IMediator mediator)
        {
            _busClient = busClient ?? throw new ArgumentNullException(nameof(busClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mediatR = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
