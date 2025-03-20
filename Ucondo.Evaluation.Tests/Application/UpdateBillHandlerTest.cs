using AutoMapper;
using FluentValidation;
using NSubstitute;
using Ucondo.Evaluation.Application;
using Ucondo.Evaluation.Application.Bills.CreateBill;
using Ucondo.Evaluation.Application.Bills.UpdateBill;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Enums;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Tests.Application
{
    public class UpdateBillHandlerTest
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;
        private readonly UpdateBillHandler _handler;

        public UpdateBillHandlerTest()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateBillHandler(_billRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid bill data When updating bill Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            var command = new UpdateBillCommand
            {
                Id = Guid.NewGuid(),
                Code = "1",
                AllowPayments = false,
                Name = "Valid Name",
                ParentBillId = null,
                Type = BillType.Despesa
            };

            var bill = new Bill
            {
                Id = command.Id,
                Code = command.Code,
                AllowPayments = command.AllowPayments,
                Name = command.Name,
                ParentBillId = command.ParentBillId,
                Type = command.Type
            };

            var result = new UpdateBillResult
            {
                Id = command.Id,
            };

            _mapper.Map<Bill>(command).Returns(bill);
            _mapper.Map<UpdateBillResult>(Arg.Any<Bill>()).Returns(result);
            _billRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(bill);
            _billRepository.UpdateAsync(Arg.Any<Bill>(), Arg.Any<CancellationToken>()).Returns(bill);

            var updateBillResult = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(updateBillResult);
            await _billRepository.Received(1).UpdateAsync(Arg.Any<Bill>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid bill id When updating bill Then throws NotFound exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            var command = new UpdateBillCommand();

            var act = () => _handler.Handle(command, CancellationToken.None);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await act();
            });
        }
    }
}
