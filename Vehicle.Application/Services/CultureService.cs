using System.Globalization;
namespace Vehicle.Application.Services
{
    public class CultureService
    {
        //private readonly IRepository _repository; 
        //public CultureService(IRepository repository) 
        //{ _repository = repository; 
        //}
        public void SetCulture(string cultureName)
        {
            if (!string.IsNullOrWhiteSpace(cultureName))
            {
                var culture = new CultureInfo(cultureName);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
        }
    }

}
