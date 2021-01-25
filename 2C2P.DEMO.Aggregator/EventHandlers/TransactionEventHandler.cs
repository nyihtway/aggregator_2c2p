using _2C2P.DEMO.Aggregator.Models;
using _2C2P.DEMO.Aggregator.Services;
using _2C2P.DEMO.Domain.Models;
using AutoMapper;
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

        public async Task Handle(TransactionEvent transaction, CancellationToken cancellationToken)
        {
            try
            {
                if (transaction != null)
                {
                    var documents = new Transaction()
                    {
                        Id = transaction.Id,
                        TransactionId = transaction.TransactionId,
                        Amount = transaction.Amount,
                        CurrencyCode = transaction.CurrencyCode,
                        TransactionDate = transaction.TransactionDate,
                        Status = transaction.Status
                    };

                    await _crudService.InsertTransaction(documents);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
