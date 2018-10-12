﻿using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System.Linq;
using System.Text;

namespace Dealership.Client.Commands.CRUD.FilterCarsCommands
{
    public class FilterByBrandCommand : PrimeCommand
    {
        public FilterByBrandCommand(IUserSession userSession) : base(userSession)
        {
        }

        public ICarService CarService { get; set; }

        public IBrandService BrandService { get; set; }

        public override string Execute(string[] parameters)
        {
            string brandName = parameters[0];

            var brand = this.BrandService.GetBrand(brandName);

            var cars = this.CarService.GetCars("asc")
                .Where(c => c.Brand.Name.ToLower() == brandName)
                .Select(c => new CarVM
                {
                    Id = c.Id,
                    BrandName = c.Brand.Name,
                    Model = c.Model,
                    EngineCap = c.EngineCapacity,
                    HorsePower = c.HorsePower,
                    ProductionDate = c.ProductionDate,
                    Price = c.Price,
                    NDoors = c.BodyType.NumberOfDoors,
                    BodyType = c.BodyType.Name,
                    Color = c.Color.Name,
                    ColorType = c.Color.ColorType.Name,
                    Fuel = c.FuelType.Name,
                    Gearbox = c.GearBox.GearType.Name,
                    NumberOfGears = c.GearBox.NumberOfGears,
                    Extras = c.CarsExtras.Select(ce => ce.Extra.Name).ToList()
                }).ToList();
            ;

            if (!cars.Any())
            {
                return $"No cars with brand {brandName}.";
            }

            var sb = new StringBuilder();

            foreach (var car in cars)
            {
                sb.AppendLine(car.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
