using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
namespace WebApiDemo.Filters
{
    public class Shirt_validateShirtIdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var shirtId = context.ActionArguments["id"] as int?;

            if (shirtId.HasValue)
            {
                if(shirtId.Value <= 0)
                {
                    context.ModelState.AddModelError("shirtId", "shirtId is invalid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Invalid shirtId",
                        Detail = "The shirtId provided is invalid. Please provide a valid shirtId."
                    };
                    context.Result = new BadRequestObjectResult(context.ModelState);                    
                }
                else if (!ShirtRepository.ShirtExists(shirtId.Value))
                {
                    context.ModelState.AddModelError("shirtId", "shirt doesnt exist.");
                    context.Result = new NotFoundObjectResult(context.ModelState);
                }
            }
        }
    }
}  