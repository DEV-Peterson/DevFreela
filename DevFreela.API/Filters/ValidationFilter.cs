using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DevFreela.API.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null)
                    return;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

                if (validator != null)
                {
                    var validationContext = new ValidationContext<object>(argument);
                    var result = validator.Validate(validationContext);

                    if (!result.IsValid)
                    {
                        List<string> errorResponse = [];
                        foreach (var error in result.Errors)
                            errorResponse.Add($"Propriedade: {error.PropertyName}, Error: {error.ErrorMessage}");

                        context.Result = new BadRequestObjectResult(errorResponse);
                    }
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Lógica depois da execução da ação
        }
    }
}