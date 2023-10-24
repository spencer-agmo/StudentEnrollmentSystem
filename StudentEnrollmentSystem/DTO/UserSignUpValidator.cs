using FluentValidation;

namespace StudentEnrollmentSystem.DTO
{
    public class UserSignUpValidator : AbstractValidator<SignUpDTO>
    {
            public UserSignUpValidator()
            {
                RuleFor(x => x.Email).NotNull();
                RuleFor(x => x.FirstName).MinimumLength(3);
                RuleFor(x => x.Email).EmailAddress();

            }
        
    }
}
