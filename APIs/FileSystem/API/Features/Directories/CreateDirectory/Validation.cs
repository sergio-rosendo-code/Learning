using FluentValidation;

namespace API.Features.Directories.CreateDirectory;

public class Validation : AbstractValidator<CreateDirectoryRq>
{
    public Validation()
    {
        RuleFor(rq => rq.Name).NotEmpty().WithMessage("Invalid Directory Name");
        RuleFor(rq => rq.Path).NotEmpty().WithMessage("Invalid Path");
    }
}