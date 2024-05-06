using Application.Data.Interfaces;
using Shared.Localization.Enums;
using Shared.Resources.Serialization.Geolocation.Argentina;
using Domain.Models.Location;
using Infrastructure.Persistence.Interfaces.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Application.Data.Seed.Location
{
    public class ArgentinaLocationSeed(IUnitOfWork unitOfWork) : IUnitOfWorkSeed
    {
        public async Task<bool> Process()
        {
            try
            {
                Country country = await CountrySeed();
                IEnumerable<State> states = await StatesSeed(country);
                IEnumerable<City> cities = await CitiesSeed(states);
                Currency currency = await CurrencySeed(country);

                await unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private async Task<Country> CountrySeed()
        {
            Country? country = await unitOfWork.Countries.Entity
                .FirstOrDefaultAsync(x => x.Name == "Argentina");

            if (country == null)
            {
                country = unitOfWork.Countries.Add(new Country
                {
                    Name = "Argentina",
                    SupportedCulture = Cultures.Spanish
                });
            }

            return country;
        }

        private async Task<IEnumerable<State>> StatesSeed(Country country)
        {
            List<State> states = await unitOfWork.States.Entity
                .Where(x => x.Country.Id == country.Id)
                .ToListAsync();

            if (states.Any())
            {
                return states;
            }

            foreach (StateJson state in StateSerialization.Process())
            {
                states.Add(new State()
                {
                    Country = country,
                    Name = state.iso_nombre,
                });
            }

            return unitOfWork.States
                .AddRange(states);
        }

        private async Task<IEnumerable<City>> CitiesSeed(IEnumerable<State> states)
        {
            List<City> cities = new();

            if (!states.Any())
            {
                return cities;
            }

            Guid countryId = states
                .First().CountryId;

            cities = await unitOfWork.Cities.Entity
                .Where(x => x.State.CountryId == countryId)
                .ToListAsync();

            if (cities.Any())
            {
                return cities;
            }

            List<CityJson> citiesSerialization = CitySerialization.Process();

            cities
                .AddRange(
                    from c in citiesSerialization
                    join s in states on c.provincia.nombre equals s.Name
                    select new City()
                    {
                        State = s,
                        Name = c.nombre,
                    });

            State? cabaState = states.FirstOrDefault(x => x.Name == "Ciudad Autónoma de Buenos Aires");
            if (cabaState != null)
            {
                cities
                    .AddRange(new List<City>(){
                        new () { State = cabaState, Name = "Agronomía" },
                        new () { State = cabaState, Name = "Almagro" },
                        new () { State = cabaState, Name = "Balvanera" },
                        new () { State = cabaState, Name = "Barracas" },
                        new () { State = cabaState, Name = "Belgrano" },
                        new () { State = cabaState, Name = "Boedo" },
                        new () { State = cabaState, Name = "Caballito" },
                        new () { State = cabaState, Name = "Chacarita" },
                        new () { State = cabaState, Name = "Coghlan" },
                        new () { State = cabaState, Name = "Colegiales" },
                        new () { State = cabaState, Name = "Constitución" },
                        new () { State = cabaState, Name = "Flores" },
                        new () { State = cabaState, Name = "Floresta" },
                        new () { State = cabaState, Name = "La Boca" },
                        new () { State = cabaState, Name = "La Paternal" },
                        new () { State = cabaState, Name = "Liniers" },
                        new () { State = cabaState, Name = "Mataderos" },
                        new () { State = cabaState, Name = "Monte Castro" },
                        new () { State = cabaState, Name = "Montserrat" },
                        new () { State = cabaState, Name = "Nueva Pompeya" },
                        new () { State = cabaState, Name = "Nuñez" },
                        new () { State = cabaState, Name = "Palermo" },
                        new () { State = cabaState, Name = "Parque Avellaneda" },
                        new () { State = cabaState, Name = "Parque Chacabuco" },
                        new () { State = cabaState, Name = "Parque Chas" },
                        new () { State = cabaState, Name = "Parque Patricios" },
                        new () { State = cabaState, Name = "Puerto Madero" },
                        new () { State = cabaState, Name = "Recoleta" },
                        new () { State = cabaState, Name = "Retiro" },
                        new () { State = cabaState, Name = "Saavedra" },
                        new () { State = cabaState, Name = "San Cristóbal" },
                        new () { State = cabaState, Name = "San Nicolás" },
                        new () { State = cabaState, Name = "San Telmo" },
                        new () { State = cabaState, Name = "Versalles" },
                        new () { State = cabaState, Name = "Villa Crespo" },
                        new () { State = cabaState, Name = "Villa Devoto" },
                        new () { State = cabaState, Name = "Villa General Mitre" },
                        new () { State = cabaState, Name = "Villa Lugano" },
                        new () { State = cabaState, Name = "Villa Luro" },
                        new () { State = cabaState, Name = "Villa Ortúzar" },
                        new () { State = cabaState, Name = "Villa Pueyrredón" },
                        new () { State = cabaState, Name = "Villa Real" },
                        new () { State = cabaState, Name = "Villa Riachuelo" },
                        new () { State = cabaState, Name = "Villa Santa Rita" },
                        new () { State = cabaState, Name = "Villa Soldati" },
                        new () { State = cabaState, Name = "Villa Urquiza" },
                        new () { State = cabaState, Name = "Villa del Parque" },
                        new () { State = cabaState, Name = "Vélez Sarsfield" },
                    });
            }

            return unitOfWork.Cities
                .AddRange(cities);
        }

        private async Task<Currency> CurrencySeed(Country country)
        {
            Currency? currency = await unitOfWork.Currencies
                .Entity.FirstOrDefaultAsync(c => c.Country == country);

            if (currency != null)
            {
                return currency;
            }

            return unitOfWork.Currencies.Add(new Currency()
            {
                Code = "ARS",
                Name = "Peso Argentino",
                Symbol = "$",
                Country = country,
                IsFiat = true
            });
        }
    }
}
