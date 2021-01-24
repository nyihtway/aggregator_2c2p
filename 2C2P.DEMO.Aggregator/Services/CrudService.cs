using _2C2P.DEMO.Domain.Models;
using _2C2P.DEMO.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2C2P.DEMO.Aggregator.Services
{
    public class CrudService : ICrudService
    {
        private readonly ITransactionRepository _transRepo;

        public CrudService(ITransactionRepository transRepo)
        {
            _transRepo = transRepo ?? throw new ArgumentNullException(nameof(transRepo));
        }

        public async Task<bool> InsertTransactions(List<Transaction> documents)
        {
            await _transRepo.InsertMany(documents);

            return true;
        }
    }
}
