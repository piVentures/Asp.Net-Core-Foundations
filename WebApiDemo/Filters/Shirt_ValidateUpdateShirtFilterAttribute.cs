using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Filters
{
    public class Shirt_ValidateUpdateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["id"] as int?;
            var shirt = context.ActionArguments["shirt"] as Models.Shirt;

            if(id.HasValue && id != shirt.ShirtId)
            {
             context.ModelState.AddModelError("ShirtId", "Shirt ID in the URL does not match the Shirt ID in the request body.");
             var problemDetails = new ValidationProblemDetails(context.ModelState)
             {
                 Status = StatusCodes.Status400BadRequest,
        
             };
                context.Result = new BadRequestObjectResult("Invalid input data.");
            }   

        }
    }
}