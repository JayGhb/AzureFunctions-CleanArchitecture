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

namespace SlottingMock.Api.Controllers
{
    public class SlotsController
    {
        private readonly IMediator _mediator;

        public SlotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("getSlots")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)][FromQuery] GetSlotsRequestDto input, HttpRequest req,
            ILogger logger)
        {
            try
            {
                GetSlotsQuery getSlotsQuery = new GetSlotsQuery(input.Date);

                string result = await _mediator.Send(getSlotsQuery);

                return new OkObjectResult(result);
            }
            catch(Exception exception) 
            {
                if (exception is ValidationException validationException)
                {
                    string[] validationErrorMessages = validationException.Errors.Select(x => x.ErrorMessage).ToArray();

                    var problemDetails = new ValidationProblemDetails
                    {
                        Instance = req.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Title = "One or more validation errors occurred.",
                        Detail = "See the errors object for details."
                    };

                    problemDetails.Errors.Add("QueryValidationErrors", validationErrorMessages);

                    return new BadRequestObjectResult(problemDetails); //the response sent to the client
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
