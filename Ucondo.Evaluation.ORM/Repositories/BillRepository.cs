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
    }
}
