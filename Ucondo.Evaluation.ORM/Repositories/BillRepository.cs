using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of IBillRepository using Entity Framework Core
    /// </summary>
    public class BillRepository : IBillRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of UserRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public BillRepository(DefaultContext context)
        {
            _context = context;
        }

        public Task<Bill> CreateAsync(Bill bill, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Bill?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Bill?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Bill> UpdateAsync(Guid id, Bill bill, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
