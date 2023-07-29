using Microsoft.Extensions.Logging;
using Moq;
using Application.Common.Interfaces;
using Application.UseCases.Queries.GetSlots;

namespace Appliaction.UnitTests
{
    public class GetSlotsQueryHandlerUnitTests
    {
        private readonly Mock<IYourExternalService> _ruleEngineServiceMock;
        private readonly Mock<ILogger<GetSlotsQueryHandler>> _loggerMock;

        public GetSlotsQueryHandlerUnitTests()
        {
            _ruleEngineServiceMock = new Mock<IYourExternalService>();
            _loggerMock = new Mock<ILogger<GetSlotsQueryHandler>>();
        }

        [Fact]
        public async void Handler_must_return_empty_string()
        {
            //Arrange
            _ruleEngineServiceMock
                .Setup(x => x.GetDataAsync(default).Result)
                .Returns(new List<string>() { "I am not empty" });
            GetSlotsQuery fakeQuery = new GetSlotsQuery("1999-05-25");
            string expectedResult = string.Empty;

            //Act
            GetSlotsQueryHandler fakeHandler = new GetSlotsQueryHandler(_ruleEngineServiceMock.Object, _loggerMock.Object);
            string actualResult = await fakeHandler.Handle(fakeQuery, default);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}