using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Application.Bills.DeleteBill
{
    public class DeleteBillHandler : IRequestHandler<DeleteBillCommand, DeleteBillResult>
    {
        private readonly IBillRepository _repository;
        private readonly IMapper _mapper;

        public DeleteBillHandler(IBillRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DeleteBillResult> Handle(DeleteBillCommand command, CancellationToken cancellationToken)
        {

            var bill = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (bill == null)
            {
                throw new NotFoundException($"Bill with ID {command.Id} not found.");
            }

            var validator = new DeleteBillCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var isDeleted = await _repository.DeleteAsync(command.Id, cancellationToken);

            return new DeleteBillResult { IsDeleted = isDeleted };
        }
    }
}
