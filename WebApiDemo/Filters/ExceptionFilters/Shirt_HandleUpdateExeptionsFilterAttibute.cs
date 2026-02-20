using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApiDemo.Filters.ExceptionFilters
{
    public class Shirt_HandleUpdateExeptionsFilterAttibute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var strShirtId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strShirtId, out int shirtId))
            {
                if (!ShirtRepository.ShirtExists(shirtId))
                {
                    context.ModelState.AddModelError("ShirtId", $"Shirt with ID {shirtId} not found.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                     context.Result = new NotFoundObjectResult(problemDetails);
                }
                
                }   
                
            }
        }
      
    }
    
