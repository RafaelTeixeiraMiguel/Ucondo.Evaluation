using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.ORM.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly DefaultContext _context;

        public BillRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Bill> CreateAsync(Bill bill, CancellationToken cancellationToken = default)
        {
            await _context.Bills.AddAsync(bill, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return bill;
        }

        public async Task<Bill?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Bills.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<Bill?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _context.Bills.FirstOrDefaultAsync(o => o.Code == code, cancellationToken);
        }

        public async Task<List<Bill>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var bills = await _context.Bills
                .Include(s => s.ParentBill)
                .ToListAsync(cancellationToken);

            return bills;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var bill = await GetByIdAsync(id, cancellationToken);
            if (bill == null)
                return false;

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Bill> UpdateAsync(Bill bill, CancellationToken cancellationToken = default)
        {
            _context.Bills.Update(bill);

            await _context.SaveChangesAsync(cancellationToken);
            return bill;
        }

        public async Task<string> GetHighestChildrenCode(Guid parentId, CancellationToken cancellationToken = default)
        {
            var children = await _context.Bills
            .Where(b => b.ParentBillId == parentId)
            .ToListAsync(cancellationToken);

            if (!children.Any())
                return null;

            var billWithMaxCode = children
            .OrderByDescending(b => GetCodeSegments(b.Code), new CodeSegmentComparer())
            .First();

            return billWithMaxCode.Code;
        }

        private List<int> GetCodeSegments(string code)
        {
            return code
                .Split('.')
                .Select(segment => int.TryParse(segment, out var num) ? num : 0)
                .ToList();
        }

        public async Task<List<string>> GetRootLevelCodesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Bills
                .Where(b => b.ParentBillId == null)
                .Select(b => b.Code)
                .ToListAsync(cancellationToken);
        }
    }

    public class CodeSegmentComparer : IComparer<List<int>>
    {
        public int Compare(List<int> x, List<int> y)
        {
            int minLength = Math.Min(x.Count, y.Count);

            for (int i = 0; i < minLength; i++)
            {
                int comparison = x[i].CompareTo(y[i]);
                if (comparison != 0)
                {
                    return comparison;
                }
            }

            return x.Count.CompareTo(y.Count);  // Se os códigos forem iguais até o nível mais profundo, o mais curto é considerado menor
        }
    }
}
