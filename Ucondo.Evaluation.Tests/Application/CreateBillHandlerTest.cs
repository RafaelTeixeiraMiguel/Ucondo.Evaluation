using AutoMapper;
using FluentValidation;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Application.Bills.CreateBill;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Enums;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Tests.Application
{
    public class CreateBillHandlerTest
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;
        private readonly CreateBillHandler _handler;

        public CreateBillHandlerTest()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateBillHandler(_billRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid bill data When creating bill Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            var command = new CreateBillCommand
            {
                Code = "1",
                AllowPayments = false,
                Name = "Valid Name",
                ParentBillId = null,
                Type = BillType.Despesa
            };

            var bill = new Bill
            {
                Id = Guid.NewGuid(),
                Code = command.Code,
                AllowPayments = false,
                Name = command.Name,
                ParentBillId = command.ParentBillId,
                Type = BillType.Despesa
            };

            var result = new CreateBillResult
            {
                Id = bill.Id,
            };

            _mapper.Map<Bill>(command).Returns(bill);
            _mapper.Map<CreateBillResult>(Arg.Any<Bill>()).Returns(result);
            _billRepository.CreateAsync(Arg.Any<Bill>(), Arg.Any<CancellationToken>()).Returns(bill);

            var createBillResult = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(createBillResult);
            await _billRepository.Received(1).CreateAsync(Arg.Any<Bill>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid bill data When creating bill Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            var command = new CreateBillCommand();
            command.Code = "";

            var act = () => _handler.Handle(command, CancellationToken.None);

            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await act();
            });
        }
    }
}
