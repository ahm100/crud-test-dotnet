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
        /*
         * public string: This specifies that the indexer returns a string and is publicly accessible.

this[string key]: This defines the indexer. The this keyword indicates it's an indexer, and [string key] specifies that it takes a string as a parameter (the key).

=> _localizer[key];: This is the expression-bodied member syntax. It means that when the indexer is accessed with a key, it returns the value from the _localizer using that key.

So, when you use localizer["Hello World"], it uses the indexer to get the localized string associated with the key "Hello World".


        we can use instead :
        public string GetString(string key) { return _localizer[key]; }
        and call (_localizer.GetString("Hello World")
         */
    }

}
