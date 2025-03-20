using AutoMapper;
using MockQueryable;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucondo.Evaluation.Application.Bills.GetSuggestedCode;
using Ucondo.Evaluation.Application.Bills.ListBill;
using Ucondo.Evaluation.Domain.Entities;
using Ucondo.Evaluation.Domain.Enums;
using Ucondo.Evaluation.Domain.Repositories;

namespace Ucondo.Evaluation.Tests.Application
{
    public class ListBillHandlerTest
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;
        private readonly ListBillHandler _handler;

        public ListBillHandlerTest()
        {
            _billRepository = Substitute.For<IBillRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new ListBillHandler(_billRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid bill data When listing bills Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            var command = new ListBillCommand();

            var parentBill = new Bill
            {
                Id = Guid.NewGuid(),
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

            var bills = new List<Bill> { parentBill, childBill };

            var result = new ListBillResult
            {
                Bills = new List<BillResult>
                {
                    new BillResult
                    {
                        Id = parentBill.Id,
                        Code = parentBill.Code,
                        AllowPayments = parentBill.AllowPayments,
                        Name = parentBill.Name,
                        ParentBill = parentBill.ParentBill,
                        Type = parentBill.Type
                    },
                    new BillResult
                    {
                        Id = childBill.Id,
                        Code = childBill.Code,
                        AllowPayments = childBill.AllowPayments,
                        Name = childBill.Name,
                        ParentBill = parentBill,
                        Type = childBill.Type
                    }
                }
            };

            _mapper.Map<List<BillResult>>(Arg.Any<Bill>()).Returns(result.Bills);
            var mockQueryable = bills.AsQueryable().BuildMock();
            _billRepository.Query().Returns(mockQueryable);

            var listBillResult = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(listBillResult);
            _billRepository.Received(1).Query();
        }
    }
}
