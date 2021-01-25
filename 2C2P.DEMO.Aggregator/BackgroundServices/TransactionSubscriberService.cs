using _2C2P.DEMO.Aggregator.Models;
using _2C2P.DEMO.Domain.Events;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace _2C2P.DEMO.Aggregator.BackgroundServices
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
            int threads = _configuration.GetValue<int>("KafkaConfig:Threads");
            int noThreads = threads > 0 ? threads : 1;

            for (int i = 0; i < noThreads; i++)
            {
                _ = Task.Run(async () => await SubscribeToKafka(stoppingToken));
                await Task.CompletedTask;
            }
        }

        private async Task SubscribeToKafka(CancellationToken cancellationToken)
        {
            var env = _configuration.GetValue<string>("Env");
            int commitPeriod = _configuration.GetValue<int>("KafkaConfig:CommitPeriod");
            if (string.IsNullOrWhiteSpace(env))
            {
                throw new ArgumentNullException(nameof(env));
            }

            await _busClient.SubscribeAsync<TransactionEvent>(async (message) => { await _mediatR.Publish(message); }, $"{typeof(TransactionEvent).Name}_{env}", cancellationToken, commitPeriod);
        }
    }
}
