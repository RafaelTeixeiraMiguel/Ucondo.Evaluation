using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ucondo.Evaluation.API.Common;
using Ucondo.Evaluation.API.Features.Bills.CreateBill;
using Ucondo.Evaluation.API.Features.Bills.DeleteBill;
using Ucondo.Evaluation.API.Features.Bills.GetSuggestedCode;
using Ucondo.Evaluation.API.Features.Bills.ListBill;
using Ucondo.Evaluation.API.Features.Bills.UpdateBill;
using Ucondo.Evaluation.Application.Bills.CreateBill;
using Ucondo.Evaluation.Application.Bills.DeleteBill;
using Ucondo.Evaluation.Application.Bills.GetSuggestedCode;
using Ucondo.Evaluation.Application.Bills.ListBill;
using Ucondo.Evaluation.Application.Bills.UpdateBill;

namespace Ucondo.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BillController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateBillResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBill([FromBody] CreateBillRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateBillRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateBillCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateBillResponse>
            {
                Success = true,
                Message = "Bill created successfully",
                Data = _mapper.Map<CreateBillResponse>(response)
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateBillResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBill(Guid id, [FromBody] UpdateBillRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBillRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateBillCommand>(request);
            command.Id = id;

            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
            {
                return NotFound(new ApiResponse { Success = false, Message = "Bill not found" });
            }

            return Ok(new ApiResponseWithData<UpdateBillResponse>
            {
                Success = true,
                Message = "Bill updated successfully",
                Data = _mapper.Map<UpdateBillResponse>(response)
            });
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<ListBillResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListBill(CancellationToken cancellationToken)
        {
            var request = new ListBillRequest();
            var validator = new ListBillRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new ListBillCommand();

            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
                return NotFound();

            var result = _mapper.Map<ListBillResult>(response);

            return Ok(new ApiResponseWithData<ListBillResult>
            {
                Success = true,
                Message = "Bill retrieved successfully",
                Data = result
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<DeleteBillResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBill(Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteBillRequest();
            var validator = new DeleteBillRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = new DeleteBillCommand(id);

            var response = await _mediator.Send(command, cancellationToken);

            var result = _mapper.Map<DeleteBillResult>(response);

            return Ok(new ApiResponseWithData<DeleteBillResult>
            {
                Success = true,
                Message = "Bill deleted successfully",
                Data = result
            });
        }

        [HttpGet("{parentId}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSuggestedCodeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSuggestedCode(Guid parentId, CancellationToken cancellationToken)
        {
            var request = new GetSuggestedCodeRequest { ParentId = parentId };
            var validator = new GetSuggestedCodeValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);


            var command = _mapper.Map<GetSuggestedCodeCommand>(request);

            var response = await _mediator.Send(command, cancellationToken);

            if (response == null)
                return NotFound();

            var result = _mapper.Map<GetSuggestedCodeResult>(response);

            return Ok(new ApiResponseWithData<GetSuggestedCodeResult>
            {
                Success = true,
                Message = "Next code successfully suggested",
                Data = result
            });
        }
    }
}
