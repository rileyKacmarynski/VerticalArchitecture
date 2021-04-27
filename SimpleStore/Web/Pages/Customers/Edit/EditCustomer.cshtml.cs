using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Customers.Edit
{
    public class EditCustomerModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public EditCustomerCommand Data { get; set; }
        public EditCustomerModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(EditCustomerQuery query)
        {
            Data = await _mediator.Send(query);
        }
    }
}
