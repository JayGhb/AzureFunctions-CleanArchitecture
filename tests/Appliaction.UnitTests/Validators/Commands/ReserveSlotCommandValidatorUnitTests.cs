using Application.UseCases.Commands.ReserveSlot;
using Application.Validators.Commands;
using FluentValidation.TestHelper;

namespace Appliaction.UnitTests.Validators.Commands
{
    public class ReserveSlotCommandValidatorUnitTests
    {
        private ReserveSlotCommandValidator validator;

        public ReserveSlotCommandValidatorUnitTests()
        {
            validator = new ReserveSlotCommandValidator();
        }

        [Fact]
        public async Task Empty_SlotId_Fails_Validation()
        {
            //Arrange
            ReserveSlotCommand fakeCommand = new ReserveSlotCommand(string.Empty);

            //Act
            TestValidationResult<ReserveSlotCommand> actualResult = await validator.TestValidateAsync(fakeCommand);

            //Assert
            actualResult.ShouldHaveValidationErrorFor(command => command.SlotId);
        }

        [Fact]
        public async Task Correct_SlotId_Passes_Validation()
        {
            //Arrange
            ReserveSlotCommand fakeCommand = new ReserveSlotCommand("ds5f-cfg69");

            //Act
            TestValidationResult<ReserveSlotCommand> actualResult = await validator.TestValidateAsync(fakeCommand);

            //Assert
            actualResult.ShouldNotHaveValidationErrorFor(command => command.SlotId);
        }
    }
}
