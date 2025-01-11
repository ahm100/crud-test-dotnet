using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Application.Features.Customers.Commands;
using Vehicle.Application.Features.Customers.Queries;
using Vehicle.Domain.Entities.Concrete;

namespace Vehicle.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() 
        {
            CreateMap<Customer, CustomerVM>().ReverseMap();
            CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
            CreateMap<CreateCustomerCommand, Customer>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore()); // We'll handle PhoneNumber formatting separately
        }
    }
}
