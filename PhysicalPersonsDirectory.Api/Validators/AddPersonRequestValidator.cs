using FluentValidation;
using PhysicalPersonsDirectory.Services.Models.Person.Add;
using PhysicalPersonsDirectory.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PhysicalPersonsDirectory.Api.Validators
{
    public class AddPersonRequestValidator : AbstractValidator<AddPersonRequest>
    {
        public AddPersonRequestValidator()
        {
            RuleFor(t => t.Fname).NotEmpty().WithMessage(RsStrings.PersonFnameIsRequred)
                                 .MinimumLength(2).WithMessage(RsStrings.PersonFnameMinLengthRestrict)
                                 .MaximumLength(50).WithMessage(RsStrings.PersonFnameMaxLengthRestrict)
                                 .Custom((fname, context) =>
                                 {
                                     if (!Regex.IsMatch(fname, "^[ა-ჰ]*$") && !Regex.IsMatch(fname, "^[a-zA-Z]*$"))
                                     {
                                         context.AddFailure(RsStrings.PersonFnameRestrict);
                                     }
                                 });

            RuleFor(t => t.Lname).NotEmpty().WithMessage(RsStrings.PersonLnameIsRequred)
                                .MinimumLength(2).WithMessage(RsStrings.PersonLnameMinLengthRestrict)
                                .MaximumLength(50).WithMessage(RsStrings.PersonLnameMaxLengthRestrict)
                                .Custom((lname, context) =>
                                {
                                    if (!Regex.IsMatch(lname, "^[ა-ჰ]*$") && !Regex.IsMatch(lname, "^[a-zA-Z]*$"))
                                    {
                                        context.AddFailure(RsStrings.PersonLnameRestrict);
                                    }
                                });

            RuleFor(t => t.PersonalNumber).NotEmpty().WithMessage(RsStrings.PersonalNumberIsRequred)
                                          .Length(10, 12).WithMessage(RsStrings.PersonalNumberLengthRestrict)
                                          .Custom((personalNumber, context) =>
                                          {
                                              if (!Regex.IsMatch(personalNumber, "^[0-9]+$"))
                                              {
                                                  context.AddFailure(RsStrings.PersonalNumberRestrict);
                                              }
                                          });

            RuleFor(t => t.BirthDate).NotNull().WithMessage(RsStrings.PersonBirthDateIsRequred)
                                     .Must((t, BirthDate) =>
                                     {
                                         var currentDate = DateTime.Now.Date;
                                         var difYear = currentDate.Year - BirthDate.Year;

                                         if (difYear < 18)
                                         {
                                             return false;
                                         }

                                         if (difYear == 18)
                                         {
                                             if (currentDate.Month < BirthDate.Month)
                                             {
                                                 return false;
                                             }

                                             if (currentDate.Month == BirthDate.Month)
                                             {
                                                 if (currentDate.Day < BirthDate.Day)
                                                 {
                                                     return false;
                                                 }
                                             }
                                         }

                                         return true;
                                     }).WithMessage(RsStrings.PersonBirthDateAgeRestrict);

            RuleForEach(t => t.PersonPhones).Custom((PersonPhoneModel, context) =>
                                            {
                                                if (string.IsNullOrEmpty(PersonPhoneModel.PhoneNumber))
                                                {
                                                    context.AddFailure(RsStrings.PhoneNumberMinLengthRestrict);
                                                }
                                                else
                                                {
                                                    var phoneNumberLength = PersonPhoneModel.PhoneNumber.Length;
                                                    if (phoneNumberLength < 4)
                                                    {
                                                        context.AddFailure(RsStrings.PhoneNumberMinLengthRestrict);
                                                    }

                                                    if (phoneNumberLength > 50)
                                                    {
                                                        context.AddFailure(RsStrings.PhoneNumberMaxLengthRestrict);
                                                    }
                                                }
                                            });
        }
    }
}
