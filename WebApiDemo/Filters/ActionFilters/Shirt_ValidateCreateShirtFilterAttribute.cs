using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models.Repositories;
using WebApiDemo.Models;

namespace WebApiDemo.Filters
{
 public class Shirt_ValidateCreateShirtFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var shirt = context.ActionArguments["shirt"] as Shirt;

            if (shirt == null){
                context.ModelState.AddModelError("shirt", "shirt data is null.");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            else
            {
                var existingShirt = ShirtRepository.GetShirtByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);
                if (existingShirt != null)
                {
                    context.ModelState.AddModelError("shirt", "A shirt with the same properties already exists.");
                    context.Result = new ConflictObjectResult(context.ModelState);
                }
            }
        }
    }
}