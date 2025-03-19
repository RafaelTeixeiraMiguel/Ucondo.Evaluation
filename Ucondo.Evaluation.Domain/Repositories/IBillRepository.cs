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
        Task<Bill> CreateAsync(Bill bill, CancellationToken cancellationToken = default);

        Task<List<Bill>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Bill?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Bill?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Bill> UpdateAsync(Bill bill, CancellationToken cancellationToken = default);

        Task<string> GetHighestChildrenCode(Guid parentId, CancellationToken cancellationToken = default);

        Task<List<string>> GetRootLevelCodesAsync(CancellationToken cancellationToken);
    }
}
