using Shared.Common.Attributes;

namespace Shared.Localization.Attributes
{
    public class CultureAttribute : StringAttribute
    {
        public CultureAttribute(string supportedCulture) : base(supportedCulture) { }
    }
}