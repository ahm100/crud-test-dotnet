using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Vehicle.Application.Features.JobAdvertisers.Commands
{
    public class UpdateJobAdvertiserCommand : IRequest
    {
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string WebsiteUrl { get; set; }
        public string ContactEmail { get; set; }

        [RegularExpression(@"^[0+]\d{0,14}$",
            ErrorMessage = "Field must start with 0 or + and have no more than 15 digits.")]
        public string ContactPhoneNumber { get; set; }
    }
}
