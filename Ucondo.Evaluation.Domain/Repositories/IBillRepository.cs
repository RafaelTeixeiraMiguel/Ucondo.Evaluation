using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Entities;

namespace Ucondo.Evaluation.Domain.Repositories
{
    public interface IBillRepository
    {
        /// <summary>
        /// Creates a new bill in the repository
        /// </summary>
        /// <param name="bill">The bill to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created bill</returns>
        Task<Bill> CreateAsync(Bill bill, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a bill by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the bill</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The bill if found, null otherwise</returns>
        Task<Bill?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a bill by their unique code
        /// </summary>
        /// <param name="code">The unique code of the bill</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The bill if found, null otherwise</returns>
        Task<Bill?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a bill from the repository
        /// </summary>
        /// <param name="id">The unique identifier of the bill to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the bill was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a bill from the repository
        /// </summary>
        /// <param name="id">The unique identifier of the bill to update</param>
        /// <param name="bill">The new bill data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated bill</returns>
        Task<Bill> UpdateAsync(Guid id, Bill bill, CancellationToken cancellationToken = default);
    }
}
