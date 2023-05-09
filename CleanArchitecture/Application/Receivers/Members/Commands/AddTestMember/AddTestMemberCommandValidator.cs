using Application.Recievers.Members.Commands.AddTestMember;
using Domain.ValueObjects;
using FluentValidation;

namespace Application.Receivers.Members.Commands.AddTestMember
{
    internal class AddTestMemberCommandValidator : AbstractValidator<AddTestMemberCommand>
    {
        public AddTestMemberCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(FirstName.MaxLength);
            
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(LastName.MaxLength);
        
        }

    }
}
