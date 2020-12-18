using FluentValidation;
using PhysicalPersonsDirectory.Common.Resources;
using PhysicalPersonsDirectory.Services.Models.Person.Add;

namespace PhysicalPersonsDirectory.Api.Validators
{
    public class AddPersonImageRequestValidator : AbstractValidator<AddPersonImageRequest>
    {
        public AddPersonImageRequestValidator()
        {
            RuleFor(t => t.PersonId).Custom((personId, context) =>
            {
                if (personId <= 0)
                {
                    context.AddFailure(RsStrings.PersonIdIsNotCorrect);
                }
            });

            RuleFor(t => t.Image).NotEmpty().WithMessage(RsStrings.PersonImageIsRequred);
        }
    }
}