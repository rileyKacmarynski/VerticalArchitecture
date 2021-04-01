using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers
{
    public class RegisterCustomer
    {
        public class UseCase : IUseCase
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}
