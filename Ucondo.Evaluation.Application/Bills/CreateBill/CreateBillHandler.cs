using AutoMapper;
using FluentValidation;
using MediatR;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Application.Bills.CreateBill
{
    public class CreateBillHandler : IRequestHandler<CreateBillCommand, CreateBillResult>
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;

        public CreateBillHandler(IBillRepository billRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _mapper = mapper;
        }

        public async Task<CreateBillResult> Handle(CreateBillCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateBillCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingBill = await _billRepository.GetByCodeAsync(command.Code, cancellationToken);
            if (existingBill != null)
                throw new InvalidOperationException($"Bill with code {command.Code} already exists");

            var bill = _mapper.Map<Bill>(command);

            var createdBill = await _billRepository.CreateAsync(bill, cancellationToken);
            var result = _mapper.Map<CreateBillResult>(createdBill);
            return result;
        }
    }
}
