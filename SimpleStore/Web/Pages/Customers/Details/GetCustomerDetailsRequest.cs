using MediatR;
using System.ComponentModel.DataAnnotations;
using Web.Models;

namespace Web.Pages.Customers.Details
{
    public record GetCustomerDetailsRequest : IRequest<Customer>
    {
        [Required]
        public int Id { get; set; }
    }
}