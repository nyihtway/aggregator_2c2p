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
        private readonly IMapper _mapper;

        public TransactionEventHandler(ICrudService crudService, IMapper mapper)
        {
            _crudService = crudService ?? throw new ArgumentNullException(nameof(crudService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = Log.ForContext<TransactionEventHandler>();
        }

        public async Task Handle(TransactionEvent transaction, CancellationToken cancellationToken)
        {
            try
            {
                if (transaction != null)
                {
                    var documents = _mapper.Map<Transaction>(transaction);
                    await _crudService.InsertTransaction(documents);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
