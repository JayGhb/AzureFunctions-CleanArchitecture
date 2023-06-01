using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using SlottingMock.Application.UseCases.Queries.GetSlots;
using FluentValidation;
using System.Linq;
using Application.Common.Dtos.Requests;
using System.Threading;

namespace SlottingMock.Api.Controllers
{
    public class SlotsController
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// //////////////////////////////// TODO NEXT: REQUEST CORELLATION WITH x-correlation-id header and FunctionInvocationFilter
        /// </summary>
        /// <param name="mediator"></param>

        public SlotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName(nameof(GetSlotsAsync))]
        public async Task<IActionResult> GetSlotsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "get")][FromQuery] GetSlotsRequestDto input, HttpRequest req,
            ILogger logger, CancellationToken cancellationToken)
        {
            try
            {
                GetSlotsQuery getSlotsQuery = new GetSlotsQuery(input.Date);
                string result = await _mediator.Send(getSlotsQuery, cancellationToken);

                return new OkObjectResult(result);
            }
            catch (Exception exception) 
            {
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
