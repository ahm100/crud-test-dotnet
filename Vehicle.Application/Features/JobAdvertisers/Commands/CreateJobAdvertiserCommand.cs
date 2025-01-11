using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Vehicle.Application.Features.JobAdvertisers.Commands
{
    public class CreateJobAdvertiserCommand : IRequest<int>
    {
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string WebsiteUrl { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhoneNumber { get; set; }
    }
}
