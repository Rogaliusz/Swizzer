using FluentValidation;
using Swizzer.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Client.Validators
{
    public class RegisterViewModelValidator : ViewModelValidatorBase<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorMessages.Required, nameof(RegisterViewModel.Email)))
                .EmailAddress()
                .WithMessage(string.Format(ValidatorMessages.HasInvalidFormat, nameof(RegisterViewModel.Email)));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorMessages.Required, nameof(RegisterViewModel.Password)))
                .Equal(x => x.RepeatPassword)
                .WithMessage(string.Format(ValidatorMessages.MustBeTheSame, nameof(RegisterViewModel.Password), nameof(RegisterViewModel.RepeatPassword)));

            RuleFor(x => x.RepeatPassword)
                .NotEmpty()
                .WithMessage(string.Format(ValidatorMessages.Required, nameof(RegisterViewModel.RepeatPassword)));
        }
    }
}
