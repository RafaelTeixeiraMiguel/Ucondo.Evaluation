using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var validator = new ListBillCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var bills = await _repository.GetAllAsync(cancellationToken);

            if (bills == null)
                throw new NotFoundException("Bills not found.");

            var result = _mapper.Map<ListBillResult>(bills);

            return result;
        }
    }
}
