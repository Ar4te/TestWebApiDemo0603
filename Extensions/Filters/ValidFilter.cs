using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SqlSugar;

namespace Extensions.Filters;

public class ValidFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {

    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var ss = context.ActionDescriptor as ControllerActionDescriptor;
            var ttt = ss.AttributeRouteInfo.Template;
            List<string> msg = new();
            foreach (var item in context.ModelState.Values)
            {
                if (item.Errors != null && item.Errors.Any())
                {
                    foreach (var error in item.Errors)
                    {
                        msg.Add(error.ErrorMessage);
                    }
                }
            }
            Dictionary<string, List<string>> list = new()
            {
                { ttt + " ocurred some errors:", msg }
            };
            context.Result = new JsonResult(Failed(list));
        }
    }
}
