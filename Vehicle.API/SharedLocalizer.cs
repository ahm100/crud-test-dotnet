using Microsoft.Extensions.Localization;
using Vehicle.API.Controllers;
using Vehicle.Application.Contracts;

namespace Vehicle.API
{
    public class SharedLocalizer : ISharedLocalizer
    {
        private readonly IStringLocalizer<JobSeekersController> _localizer;

        public SharedLocalizer(IStringLocalizer<JobSeekersController> localizer)
        {
            _localizer = localizer;
        }

        public string this[string key] => _localizer[key];
    }

}
