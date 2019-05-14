using Core.Template.Application.Commands;
using Core.Template.Configuration;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Template.Application.Validations
{
    /// <summary>
    /// 
    /// </summary>
    public class AddUserDemoCommandValidator : AbstractValidator<AddUserDemoCommand>
    {
        /// <summary>
        /// 
        /// </summary>
        public AddUserDemoCommandValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage(StaticConst.UserNameNotNullAndEmpty);
        }
    }
}
