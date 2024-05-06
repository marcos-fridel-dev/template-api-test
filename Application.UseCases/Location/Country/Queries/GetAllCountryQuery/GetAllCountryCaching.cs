using Application.UseCases.Interfaces.Middlewares.Behavior;

namespace Application.UseCases.Location.Country.Queries.GetAllCountryQuery
{
    public class GetAllCountryCaching : ICachingBehavior
    {
        public string Key => "GetAllCountryUseCase";
    }
}