using Moq;
using SlottingMock.Application.Common.Interfaces;
using SlottingMock.Application.UseCases.Queries.GetSlots;

namespace Appliaction.UnitTests
{
    public class GetSlotsQueryHandlerUnitTests
    {
        private readonly Mock<IDynamicsCrmService> _ruleEngineServiceMock;

        public GetSlotsQueryHandlerUnitTests()
        {
            _ruleEngineServiceMock = new Mock<IDynamicsCrmService>();
        }

        [Fact]
        public async void Handler_must_return_empty_string()
        {
            //Arrange
            _ruleEngineServiceMock.Setup(x => x.GetDataAsync()).Returns(new List<string>() { "I am not empty" });
            GetSlotsQuery fakeQuery = new GetSlotsQuery("1999-05-25");
            string expectedResult = string.Empty;

            //Act
            GetSlotsQueryHandler fakeHandler = new GetSlotsQueryHandler(_ruleEngineServiceMock.Object);
            string actualResult = await fakeHandler.Handle(fakeQuery, default);

            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}