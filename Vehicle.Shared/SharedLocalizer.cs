using Microsoft.Extensions.Localization;

namespace Vehicle.Shared.Resources
{
    public class SharedLocalizer<T> : ISharedLocalizer<T>
    {
        private readonly IStringLocalizer<T> _localizer;

        public SharedLocalizer(IStringLocalizer<T> localizer)
        {
            _localizer = localizer;
        }

        public string this[string key] => _localizer[key];
    }
}
