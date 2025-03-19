using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Application.Bills.ListBill
{
    public class ListBillHandler : IRequestHandler<ListBillCommand, ListBillResult>
    {
        private readonly IBillRepository _repository;
        private readonly IMapper _mapper;

        public ListBillHandler(IBillRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ListBillResult> Handle(ListBillCommand request, CancellationToken cancellationToken)
        {
            var query = _repository.Query();

            if (!string.IsNullOrWhiteSpace(request.Code))
                query = query.Where(b => b.Code == request.Code);

            if (!string.IsNullOrWhiteSpace(request.Name))
                query = query.Where(b => b.Name.Contains(request.Name));

            if (!string.IsNullOrWhiteSpace(request.CodeContains))
                query = query.Where(b => b.Code.Contains(request.CodeContains));

            if (request.AllowPayments.HasValue)
                query = query.Where(b => b.AllowPayments == request.AllowPayments.Value);

            if (request.Type.HasValue)
                query = query.Where(b => b.Type == request.Type.Value);

            if (request.Types != null && request.Types.Any())
                query = query.Where(b => request.Types.Contains(b.Type));

            if (request.ParentBillId.HasValue)
                query = query.Where(b => b.ParentBillId == request.ParentBillId.Value);

            if (request.HasParent.HasValue)
            {
                if (request.HasParent.Value)
                    query = query.Where(b => b.ParentBillId != null);
                else
                    query = query.Where(b => b.ParentBillId == null);
            }

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                var property = typeof(Bill).GetProperty(request.OrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                {
                    query = request.Desc
                        ? query.OrderByDescending(e => EF.Property<object>(e, property.Name))
                        : query.OrderBy(e => EF.Property<object>(e, property.Name));
                }
            }

            query = query.Include(b => b.ParentBill);

            var skip = (request.Page - 1) * request.PageSize;
            var totalItems = await query.CountAsync(cancellationToken);
            var items = await query.Skip(skip).Take(request.PageSize).ToListAsync(cancellationToken);

            var mappedItems = _mapper.Map<List<BillResult>>(items);

            return new ListBillResult
            {
                Bills = mappedItems,
                TotalItems = totalItems,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}
