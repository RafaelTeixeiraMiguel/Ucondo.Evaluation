using AutoMapper;
using FluentValidation;
using MediatR;
using Ucondo.Evaluation.Application.Bills.CreateBill;
using Ucondo.Evaluation.Domain.Repositories;
using Ucondo.Evaluation.Domain.Validation;

namespace Ucondo.Evaluation.Application.Bills.UpdateBill
{
    public class UpdateBillHandler : IRequestHandler<UpdateBillCommand, UpdateBillResult>
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;

        public UpdateBillHandler(IBillRepository billRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _mapper = mapper;
        }

        public async Task<UpdateBillResult> Handle(UpdateBillCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateBillCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var bill = await _billRepository.GetByIdAsync(command.Id, cancellationToken);

            if (bill == null)
            {
                throw new NotFoundException($"Bill with ID {command.Id} not found.");
            }

            if (bill.ParentBillId != null && bill.ParentBillId != Guid.Empty)
            {
                var parentValidator = new ParentBillValidator(_billRepository);
                var parentValidationResult = await parentValidator.ValidateAsync(bill, cancellationToken);

                if (!parentValidationResult.IsValid)
                    throw new ValidationException(parentValidationResult.Errors);
            }

            _mapper.Map(command, bill);

            var updateBill = await _billRepository.UpdateAsync(bill, cancellationToken);

            var result = _mapper.Map<UpdateBillResult>(updateBill);

            return result;
        }
    }
}
