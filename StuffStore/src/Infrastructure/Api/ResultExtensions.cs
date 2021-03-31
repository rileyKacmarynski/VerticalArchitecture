using Application.Shared.ResultType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Api
{
    public static class ResultExtensions
    {
        // I could do this in an action filter, but then the controller would return
        // a Result object instead of an ActionResult, which I'm not a big fan of.
        // On the other hand we're passing an instance of the controller in here,
        // which is also kind of smelly. 

        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            return result.GetValue<IActionResult>(
                onSuccess: value => new OkObjectResult(value),
                onFail: result =>
                {
                    if (result.Status == ResultStatuses.NotFound)
                    {
                        return new NotFoundResult();
                    }
                    else
                    {
                        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                    }
                });
        }

        // Since there is no value associated with this result it probably modified data.
        // we should check to make sure there are no validation exceptions. 
        public static IActionResult ToActionResult(
            this Result result, 
            Func<Result, IActionResult> onSuccess,
            Func<Result, IActionResult> onError)
        {
            if(result.Status == ResultStatuses.Invalid)
            {
                var errorDictionary = new ModelStateDictionary();
                foreach(var error in result.ValidationErrors)
                {
                    errorDictionary.AddModelError(error.Identifier, error.Error);
                }

                return new BadRequestObjectResult(errorDictionary);
            }

            if(result.Status == ResultStatuses.InvariantViolation)
            {
                return new ConflictResult();
            }

            return result.Success 
                ? onSuccess(result) 
                : onError(result);
        }
    }
}
