using _2C2P.DEMO.Aggregator.Models;
using _2C2P.DEMO.Aggregator.Services;
using _2C2P.DEMO.Domain.Models;
using MediatR;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace _2C2P.DEMO.Aggregator.EventHandlers
{
    public class TransactionEventHandler : INotificationHandler<TransactionEvent>
    {
        private readonly ILogger _logger;
        private readonly ICrudService _crudService;

        public TransactionEventHandler(ICrudService crudService)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
            _logger = Log.ForContext<TransactionEventHandler>();
        }

        public async Task Handle(TransactionEvent tEvent, CancellationToken cancellationToken)
        {
            try
            {
                if (tEvent != null)
                {
                    await _crudService.InsertTransactions(tEvent.Transactions);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
