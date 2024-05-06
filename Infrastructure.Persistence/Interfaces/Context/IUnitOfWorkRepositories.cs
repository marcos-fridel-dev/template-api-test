using Infrastructure.Persistence.Repositories.Authentication;
using Infrastructure.Persistence.Repositories.Location;
using Infrastructure.Persistence.Repositories.Security;

namespace Infrastructure.Persistence.Interfaces.Context
{
    public partial interface IUnitOfWork
    {
        #region AUTHENTICATION
        LoginRepository Login { get; }
        #endregion

        #region LOCATION
        CityRepository Cities { get; }
        CountryRepository Countries { get; }
        CurrencyRepository Currencies { get; }
        HolidayRepository Holidays { get; }
        StateRepository States { get; }
        #endregion

        #region SECURITY
        RoleRepository Roles { get; }
        UserRepository Users { get; }
        #endregion
    }
}