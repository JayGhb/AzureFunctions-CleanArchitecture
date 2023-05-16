using Moq;
using SlottingMock.Application.Common.Interfaces;
using SlottingMock.Application.UseCases.Queries.GetSlots;

namespace Appliaction.UnitTests
{
    public class GetSlotsQueryHandlerUnitTests
    {
        private readonly Mock<IRuleEngineService> _ruleEngineServiceMock;

        public GetSlotsQueryHandlerUnitTests()
        {
            _ruleEngineServiceMock = new Mock<IRuleEngineService>();
        }

        [Fact]
        public async void Handler_must_return_empty_string()
        {
            //Arrange
            _ruleEngineServiceMock.Setup(x => x.GetSettings()).Returns(new List<string>() { "I am not empty" });
            GetSlotsQuery fakeQuery = new GetSlotsQuery();
            string expectedResult = string.Empty;

            //Act
            GetSlotsQueryHandler fakeHandler = new GetSlotsQueryHandler(_ruleEngineServiceMock.Object);
            string actualResult = await fakeHandler.Handle(fakeQuery, default);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}