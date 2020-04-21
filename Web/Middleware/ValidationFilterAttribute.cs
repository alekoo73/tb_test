using Core.Interfaces;
using Core.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Models;

namespace Web.Middleware
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private readonly IPersonService _personService;
        private readonly IAppLogger<PersonService> _logger;

        public ValidationFilterAttribute(IPersonService personService, IAppLogger<PersonService> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is IWebModel);
            var action = context.ActionDescriptor.DisplayName;
            switch(param.Value)
            {
                case IFormFile file:
                  if (file.Length==0)
                    {
                        context.ModelState.AddModelError("file", ((PersonsController)context.Controller).Localizer["Required"]);
                        context.Result = new BadRequestObjectResult(context.ModelState);
                    }
                    break;
                case null:
                    switch (action)
                    {
                        case string a when a.Contains("GetPersonAsync"):
                        case string b when b.Contains("DeletePersonAsync"):
                            if ((int)context.ActionArguments["id"]==0)
                            {
                                context.ModelState.AddModelError("id", ((PersonsController)context.Controller).Localizer["Required"]);
                                context.Result = new BadRequestObjectResult(context.ModelState);
                            }
                            break;
                     
                    }
                    break;
                case PersonModel person:
                    switch (action)
                    {
                        case string a when a.Contains("AddPersonAsync"):
                            if (_personService.FindDuplicate(person.IdNumber).Result)
                            {
                                _logger.LogInformation($"Duplicate Id detected {person.IdNumber}");
                                context.ModelState.AddModelError("IdNumber", ((PersonsController)context.Controller).Localizer["Duplicate"]);
                                context.Result = new BadRequestObjectResult(context.ModelState);
                            }
                            break;
                        case string a when a.Contains("UpdatePersonAsync"):
                            if (person.Id==0)
                            {
                                _logger.LogInformation($"Id not specified {person.IdNumber} {person.FirstName} {person.LastName}");
                                context.ModelState.AddModelError("IdNumber", ((PersonsController)context.Controller).Localizer["Required"]);
                                context.Result = new BadRequestObjectResult(context.ModelState);
                            }
                            if (_personService.FindDuplicate(person.IdNumber,person.Id).Result)
                            {
                                _logger.LogInformation($"Duplicate Id detected {person.IdNumber}");
                                context.ModelState.AddModelError("IdNumber", ((PersonsController)context.Controller).Localizer["Duplicate"]);
                                context.Result = new BadRequestObjectResult(context.ModelState);
                            }
                            break;
                    }               
                    break;
            };
            if (param.Value != null)
            {

            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
