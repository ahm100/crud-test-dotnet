﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Domain.Common;

namespace Vehicle.Domain.Entities.Concrete
{
    public class Customer : EntityBase
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }// E.164 format
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

    }
}
