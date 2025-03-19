using AutoMapper;
using FluentValidation;
using MediatR;
using Ucondo.Evaluation.Application.Bills.CreateBill;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Application.Bills.GetSuggestedCode
{
    public class GetSuggestedCodeHandler : IRequestHandler<GetSuggestedCodeCommand, GetSuggestedCodeResult>
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;

        public GetSuggestedCodeHandler(IBillRepository billRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _mapper = mapper;
        }

        public async Task<GetSuggestedCodeResult> Handle(GetSuggestedCodeCommand command, CancellationToken cancellationToken)
        {
            var validator = new GetSuggestedCodeCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var parentBill = await _billRepository.GetByIdAsync(command.ParentId, cancellationToken);
            if (parentBill == null)
                throw new NotFoundException("Parent bill not found.");

            var currentCode = await _billRepository.GetHighestChildrenCode(parentBill.Id, cancellationToken);

            while (true)
            {

                if (currentCode == null)
                {
                    return new GetSuggestedCodeResult
                    {
                        Code = $"{parentBill.Code}.1"
                    };
                }

                var codeParts = currentCode.Split('.').ToList();
                var lastSegment = int.Parse(codeParts.Last());

                if(lastSegment < 999)
                {
                    codeParts[codeParts.Count - 1] = (lastSegment + 1).ToString();
                    var nextCode = string.Join(".", codeParts);
                    return new GetSuggestedCodeResult
                    {
                        Code = nextCode
                    };
                }

                currentCode = currentCode.Replace($".{lastSegment.ToString()}", "");
            }
        }
    }
}
