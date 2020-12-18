using FluentValidation;
using PhysicalPersonsDirectory.Common.Resources;
using PhysicalPersonsDirectory.Services.Models.Person.Edit;

namespace PhysicalPersonsDirectory.Api.Validators
{
    public class EditPersonImageRequestValidator : AbstractValidator<EditPersonImageRequest>
    {
        public EditPersonImageRequestValidator()
        {
            RuleFor(t => t.PersonId).NotEmpty().WithMessage(RsStrings.PersonFnameIsRequred)
                                 .Custom((personId, context) =>
                                 {
                                     if (personId < 0)
                                     {
                                         context.AddFailure(RsStrings.PersonIdIsNotCorrect);
                                     }
                                 });

            RuleFor(t => t.Image).NotEmpty().WithMessage(RsStrings.PersonLnameIsRequred);
        }
    }
}