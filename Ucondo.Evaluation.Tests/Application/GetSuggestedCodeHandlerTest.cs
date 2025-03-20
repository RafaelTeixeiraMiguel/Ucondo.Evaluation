using AutoMapper;
using FluentValidation;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Application;
using Ucondo.Evaluation.Application.Bills.CreateBill;
using Ucondo.Evaluation.Application.Bills.GetSuggestedCode;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Enums;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Tests.Application
{
    public class GetSuggestedCodeHandlerTest
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;
        private readonly GetSuggestedCodeHandler _handler;

        public GetSuggestedCodeHandlerTest()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetSuggestedCodeHandler(_billRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid code When suggesting next code Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            var command = new GetSuggestedCodeCommand
            {
                ParentId = Guid.NewGuid()
            };

            var parentBill = new Bill
            {
                Id = command.ParentId,
                Code = "1",
                AllowPayments = false,
                Name = "Valid name",
                ParentBillId = null,
                Type = BillType.Despesa
            };

            var childBill = new Bill
            {
                Id = Guid.NewGuid(),
                Code = "1.1",
                AllowPayments = true,
                Name = "Valid name",
                ParentBillId = parentBill.Id,
                Type = BillType.Despesa
            };

            var result = new GetSuggestedCodeResult
            {
                Code = "1.2"
            };

            _mapper.Map<GetSuggestedCodeResult>(Arg.Any<Bill>()).Returns(result);
            _billRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(parentBill);
            _billRepository.GetHighestChildrenCode(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(childBill.Code);

            var getSuggestedCodeResult = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(getSuggestedCodeResult);
            Assert.Equal(getSuggestedCodeResult.Code, result.Code);
        }

        [Fact(DisplayName = "Given invalid parent id When suggesting next code Then throws NotFoundException exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            var command = new GetSuggestedCodeCommand
            {
                ParentId = Guid.Empty
            };

            var act = () => _handler.Handle(command, CancellationToken.None);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await act();
            });
        }
    }
}
