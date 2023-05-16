using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using SlottingMock.Application.UseCases.Queries.GetSlots;
using SlottingMock.Application.Common.Dtos;
using FluentValidation;
using System.Linq;

namespace SlottingMock.Api.Controllers
{
    public class SlotsController
    {
        private readonly IMediator _mediator;

        public SlotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("getSlot")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)][FromBody] InputDto input, HttpRequest req,
            ILogger log)
        {
            try
            {
                GetSlotsQuery getSlotsQuery = new GetSlotsQuery()
                {
                    QueryPropertyA = input.PropertyA,
                    QueryPropertyB = input.PropertyB
                };

                var result = await _mediator.Send(getSlotsQuery);

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
                        Detail = $"{string.Join('-', validationErrorMessages)}"
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
