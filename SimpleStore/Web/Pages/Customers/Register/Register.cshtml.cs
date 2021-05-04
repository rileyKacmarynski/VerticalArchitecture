using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Pages.Customers.Register;

namespace MyApp.Namespace
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public RegisterCustomerCommand Data { get; set; }

        public DetailsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _mediator.Send(Data);

            return RedirectToPage("Index");
        }
    }
}
