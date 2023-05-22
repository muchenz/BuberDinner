using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Common.Behaviors;
public class ValidatinBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidatinBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }


        var errors = validationResult.Errors.ConvertAll(validationFailure =>
                Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));

        return (dynamic)errors;
    }
}


//public class ValidateRegisterCommandBehavior : IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
//{
//    private readonly IValidator<RegisterCommand> _validator;

//    public ValidateRegisterCommandBehavior(IValidator<RegisterCommand> validator)
//    {
//        _validator = validator;
//    }

//    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, 
//        CancellationToken cancellationToken, RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next)
//    {

//        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

//        if (validationResult.IsValid)
//        {
//            await next();
//        }


//        var errors = validationResult.Errors.ConvertAll(validationFailure =>
//                Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));

//        return errors;

//    }
//}