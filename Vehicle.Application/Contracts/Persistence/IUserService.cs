using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Application.Contracts.Persistence
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterModel model); 
        Task<string> LoginUserAsync(LoginModel model);
    }
}
