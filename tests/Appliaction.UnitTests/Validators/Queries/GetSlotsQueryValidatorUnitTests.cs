using FluentValidation.TestHelper;
using SlottingMock.Application.UseCases.Queries.GetSlots;
using SlottingMock.Application.Validators.Queries;

namespace Appliaction.UnitTests.Validators.Queries
{
    public class GetSlotsQueryValidatorUnitTests
    {
        private GetSlotsQueryValidator validator;

        public GetSlotsQueryValidatorUnitTests()
        {
            validator = new GetSlotsQueryValidator();
        }

        [Fact]
        public async Task Empty_Date_Fails_Validation()
        {
            //Arrange
            GetSlotsQuery fakeQuery = new GetSlotsQuery(string.Empty);

            //Act
            TestValidationResult<GetSlotsQuery> actualResult = await validator.TestValidateAsync(fakeQuery);

            //Assert
            actualResult.ShouldHaveValidationErrorFor(query => query.Date);
        }

        [Fact]
        public async Task Wrong_format_Date_Fails_Validation()
        {
            //Arrange
            GetSlotsQuery fakeQuery = new GetSlotsQuery("25-05-2006");

            //Act
            TestValidationResult<GetSlotsQuery> actualResult = await validator.TestValidateAsync(fakeQuery);

            //Assert
            actualResult.ShouldHaveValidationErrorFor(query => query.Date);
        }

        [Fact]
        public async Task Correct_Date_Passes_Validation()
        {
            //Arrange
            GetSlotsQuery fakeQuery = new GetSlotsQuery("2006-04-25");

            //Act
            TestValidationResult<GetSlotsQuery> actualResult = await validator.TestValidateAsync(fakeQuery);

            //Assert
            actualResult.ShouldNotHaveValidationErrorFor(query => query.Date);
        }
    }
}
