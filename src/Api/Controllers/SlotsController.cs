using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using Application.UseCases.Queries.GetSlots;
using FluentValidation;
using System.Linq;
using Application.Common.Dtos.Requests;
using System.Threading;
using Newtonsoft.Json;
using Application.UseCases.Commands.ReserveSlot;

namespace Api.Controllers
{
    public class SlotsController : FunctionBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SlotsController> _logger;

        public SlotsController(IMediator mediator, ILogger<SlotsController> logger, IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName(nameof(GetSlotsAsync))]
        public async Task<IActionResult> GetSlotsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "get")][FromQuery] GetSlotsRequestDto input, HttpRequest req,
            ILogger logger, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("{functionName} processing request {input}", nameof(GetSlotsAsync), JsonConvert.SerializeObject(input));
                GetSlotsQuery getSlotsQuery = new GetSlotsQuery(input.Date);
                string result = await _mediator.Send(getSlotsQuery, cancellationToken);
                _logger.LogInformation("{functionName} function result: {result}", nameof(GetSlotsAsync), result);
                return new OkObjectResult(result);
            }
            catch (Exception exception) 
            {
                _logger.LogError(exception.Message + " " + exception.StackTrace);

                if (exception is ValidationException validationException)
                {
                    string[] validationErrorMessages = validationException.Errors.Select(x => x.ErrorMessage).ToArray();

                    ValidationProblemDetails problemDetails = new ValidationProblemDetails
                    {
                        Instance = req.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Title = "One or more validation errors occurred.",
                        Detail = "See the errors object for details."
                    };

                    problemDetails.Errors.Add("QueryValidationErrors", validationErrorMessages);

                    return new BadRequestObjectResult(problemDetails); //the response sent to the client
                }
                else if (exception is OperationCanceledException operationCanceledException)
                {
                    ///Returning a ProblemDetails response upon request cancellation might not make sense in some cases
                    ///as the client is not waiting for a response anymore. You might optionally return partial or interim results
                    ///that have e.g. been fetched from a database. As with almost everything else, it depends on the nature of the operation
                    ///and the desired behavior of the application.

                    int statusCode = 499;

                    ProblemDetails problemDetails = new ProblemDetails
                    {
                        Instance = req.Path,
                        Status = statusCode,
                        Title = "Request was canceled by the client."
                    };

                    return new ObjectResult(problemDetails) { StatusCode = statusCode };
                }

                return new ContentResult()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Content = $"Message: Internal Error.",
                    ContentType = "application/json"
                };
            }
        }

        [FunctionName(nameof(ReserveSlotAsync))]
        public async Task<IActionResult> ReserveSlotAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "reserve")][FromBody] ReserveSlotRequestDto input, HttpRequest req,
            ILogger logger, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("{functionName} processing request {input}", nameof(ReserveSlotAsync), JsonConvert.SerializeObject(input));
                ReserveSlotCommand reserveSlotCommand = new ReserveSlotCommand(input.SlotId);
                Unit result = await _mediator.Send(reserveSlotCommand, cancellationToken);
                _logger.LogInformation("{functionName} function result: {result}", nameof(ReserveSlotAsync), result);
                return new OkObjectResult(result);
            }
            catch(Exception exception)
            {
                _logger.LogError(exception.Message + " " + exception.StackTrace);

                if (exception is ValidationException validationException)
                {
                    string[] validationErrorMessages = validationException.Errors.Select(x => x.ErrorMessage).ToArray();

                    ValidationProblemDetails problemDetails = new ValidationProblemDetails
                    {
                        Instance = req.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Title = "One or more validation errors occurred.",
                        Detail = "See the errors object for details."
                    };

                    problemDetails.Errors.Add("QueryValidationErrors", validationErrorMessages);

                    return new BadRequestObjectResult(problemDetails); //the response sent to the client
                }
                else if (exception is OperationCanceledException operationCanceledException)
                {
                    ///Returning a ProblemDetails response upon request cancellation might not make sense in some cases
                    ///as the client is not waiting for a response anymore. You might optionally return partial or interim results
                    ///that have e.g. been fetched from a database. As with almost everything else, it depends on the nature of the operation
                    ///and the desired behavior of the application.

                    int statusCode = 499;

                    ProblemDetails problemDetails = new ProblemDetails
                    {
                        Instance = req.Path,
                        Status = statusCode,
                        Title = "Request was canceled by the client."
                    };

                    return new ObjectResult(problemDetails) { StatusCode = statusCode };
                }

                return new ContentResult()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Content = $"Message: Internal Error.",
                    ContentType = "application/json"
                };
            }
        }
    }
}
