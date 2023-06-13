using Microsoft.AspNetCore.Mvc.Filters;

namespace Extensions.Filters;

public class CacheFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        throw new NotImplementedException();
    }
}
