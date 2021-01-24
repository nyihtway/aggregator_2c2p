using _2C2P.DEMO.Aggregator.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using _2C2P.DEMO.Aggregator.Services;

namespace _2C2P.DEMO.Aggregator.EventHandlers
{
    public class TransactionEventHandler : INotificationHandler<Models.TransactionEvent>
    {
        private readonly ILogger _logger;
        private readonly ICrudService _crudService;

        public TransactionEventHandler(ICrudService crudService)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
            _logger = Log.ForContext<TransactionEventHandler>();
        }

        public async Task Handle(TransactionEvent transaction, CancellationToken cancellationToken)
        {
            try
            {
                if(transaction != null)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
