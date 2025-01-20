using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Application.Contracts
{
    public interface ISharedLocalizer
    {
        string this[string key] { get; }
    }

}
