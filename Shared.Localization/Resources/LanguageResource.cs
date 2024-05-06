using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace Shared.Localization.Resources
{
    public class LanguageResource
    {
        public static IEnumerable<CultureInfo> GetAvailableLanguages()
        {
            List<CultureInfo> result = new List<CultureInfo>();

            ResourceManager rm = new ResourceManager(typeof(Languages.Language));

            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            foreach (CultureInfo culture in cultures)
            {
                try
                {
                    if (culture.Equals(CultureInfo.InvariantCulture)) continue;

                    ResourceSet? rs = rm.GetResourceSet(culture, true, false);
                    if (rs != null)
                        result.Add(culture);
                }
                catch (CultureNotFoundException)
                {
                }
            }

            return result;
        }
    }
}