using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Application;
using Ucondo.Evaluation.Application.Bills.CreateBill;
using Ucondo.Evaluation.Application.Bills.DeleteBill;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Enums;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Tests.Application
{
    public class DeleteBillHandlerTest
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;
        private readonly DeleteBillHandler _handler;

        public DeleteBillHandlerTest()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new DeleteBillHandler(_billRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid bill id When deleting bill Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            var command = new DeleteBillCommand(Guid.NewGuid());

            var bill = new Bill
            {
                Id = command.Id,
                Code = "1",
                AllowPayments = false,
                Name = "Valid name",
                ParentBillId = null,
                Type = BillType.Despesa
            };

            var result = new DeleteBillResult
            {
                IsDeleted = true,
            };

            _billRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(bill);

            var deleteBillResult = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(deleteBillResult);
            await _billRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid bill id When deleting bill Then throws NotFound exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            var command = new DeleteBillCommand(Guid.Empty);

            var act = () => _handler.Handle(command, CancellationToken.None);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await act();
            });
        }
    }
}
