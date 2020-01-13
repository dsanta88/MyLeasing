using MyLeasing.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        public SeedDb(DataContext context)
        {
            _context = context;
        }

        //Garantiza que la base de datos esta creada.
        public async Task SeedAsync()
        {      
            await _context.Database.EnsureCreatedAsync();
            await CheckPropertyTypesAsync();
            await CheckOwnersAsync();
            await CheckLesseesAsync();
            await CheckPropertiesAsync();
        }

        private async Task CheckPropertyTypesAsync()
        {
            if (!_context.PropertyTypes.Any())
            {
                _context.PropertyTypes.Add(new Entities.PropertyType { Name="Apartamento"});
                _context.PropertyTypes.Add(new Entities.PropertyType { Name = "Casa" });
                _context.PropertyTypes.Add(new Entities.PropertyType { Name = "Negocio" });
                await _context.SaveChangesAsync();
            }
        }



        private async Task CheckOwnersAsync()
        {
            if (!_context.Owners.Any())
            {
                AddOwner("3525411", "Juan", "Velez", "35212", "316 279 92 24", "Calle 50");
                AddOwner("63521", "Pedro", "Granada", "336522", "319 279 92 24", "Calle 36");
                AddOwner("35214", "Camilo", "Otalvaro", "635241", "319 279 92 24", "Calle 36");
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
        {
            _context.Owners.Add(new Owner
            {
                Address = address,
                CellPhone = cellPhone,
                Document = document,
                FirstName = firstName,
                FixedPhone = fixedPhone,
                LastName = lastName
            });
        }


        private async Task CheckPropertiesAsync()
        {
            var owner = _context.Owners.FirstOrDefault();
            var propertyType = _context.PropertyTypes.FirstOrDefault();

            if(!_context.properties.Any())
            {
                AddProperty("Calle 50 #23","Poblado",owner,propertyType,800000M,2,72,4);
                AddProperty("Calle 50 #23", "Laureles", owner, propertyType, 7000000M, 2, 81, 4);
                await _context.SaveChangesAsync();
            }
        }

        private void AddProperty(
           string address,
           string neighborhood,
           Owner owner,
           PropertyType propertyType,
           decimal price,
           int rooms,
           int squareMeters,
           int stratum)
        {
            _context.properties.Add(new Property
            {
                Address = address,
                HasParkingLot = true,
                IsAvailable = true,
                Neighborhood = neighborhood,
                Owner = owner,
                PropertyType = propertyType,
                Price = price,
                Rooms = rooms,
                SquareMeters = squareMeters,
                Stratum = stratum
            });
        }


        private async Task CheckLesseesAsync()
        {
            if(!_context.Lessees.Any())
            {
                AddLessee("8562521","Ramon","Gamboa","3779166","316 279 92 24","Calle 50");
                AddLessee("3652418", "Carmenza", "Ocampo", "3652425", "319 279 92 24", "Calle 36");
                AddLessee("3625218", "Andres", "Perez", "3652425", "319 279 92 24", "Calle 36");
                await _context.SaveChangesAsync();
            }
        }

        private void AddLessee(string document, string firstName, string lastName, string fixedPhone, string cellPhone, string address)
        {
            _context.Lessees.Add(new Lessee
            {
                Address = address,
                CellPhone=cellPhone,
                Document=document,
                FirstName=firstName,
                FixedPhone=fixedPhone,
                LastName=lastName
            });
        }


       



    }
}
