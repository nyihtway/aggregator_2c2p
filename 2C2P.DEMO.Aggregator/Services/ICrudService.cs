using _2C2P.DEMO.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2C2P.DEMO.Aggregator.Services
{
    public interface ICrudService
    {
        Task<bool> InsertTransaction(Transaction document);
        Task<bool> InsertTransactions(IEnumerable<Transaction> documents);
    }
}
