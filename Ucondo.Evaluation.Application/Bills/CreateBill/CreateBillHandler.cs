using AutoMapper;
using FluentValidation;
using MediatR;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Application.Bills.CreateBill
{
    /// <summary>
    /// Handler for processing CreateBillCommand requests
    /// </summary>
    public class CreateBillHandler : IRequestHandler<CreateBillCommand, CreateBillResult>
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateBillHandler
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for CreateUserCommand</param>
        public CreateBillHandler(IBillRepository billRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the CreateBillCommand request
        /// </summary>
        /// <param name="command">The CreateBill command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created bill details</returns>
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
