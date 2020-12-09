using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace MoviesApp.Filters
{
    public class AgeFilters : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var birthdayStr = context.HttpContext.Request.Form["Birthdate"];
            var birthday = DateTime.Parse(birthdayStr);
            var today = DateTime.Now.Date;
            var deltaYear = Math.Abs(birthday.Year - today.Year);

            if (deltaYear < 7 || deltaYear > 99)
            {
                context.Result = new BadRequestResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
