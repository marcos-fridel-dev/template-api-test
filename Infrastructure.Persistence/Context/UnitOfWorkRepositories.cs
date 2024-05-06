using Infrastructure.Persistence.Interfaces.Context;
using Infrastructure.Persistence.Repositories.Authentication;
using Infrastructure.Persistence.Repositories.Location;
using Infrastructure.Persistence.Repositories.Security;

namespace Infrastructure.Persistence.Context
{
    public partial class UnitOfWork : IUnitOfWork

    {
        #region AUTHENTICATION
        public LoginRepository Login => new LoginRepository(_context);

        #endregion

        #region LOCATIONS
        public CityRepository Cities => new CityRepository(_context);
        public CountryRepository Countries => new CountryRepository(_context);
        public CurrencyRepository Currencies => new CurrencyRepository(_context);
        public HolidayRepository Holidays => new HolidayRepository(_context);
        public StateRepository States => new StateRepository(_context);
        #endregion

        #region SECURITY
        public RoleRepository Roles => new RoleRepository(_context);
        public UserRepository Users => new UserRepository(_context);

        #endregion
    }
}