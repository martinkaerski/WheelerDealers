﻿using Dealership.Client.Commands.Abstract;
using Dealership.Client.ViewModels;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Client.Commands.CRUD
{
    public class ListCommand : PrimeCommand
    {
        public ICarService Service { get; set; }

        public override string Execute(string[] parameters)
        {
            if (parameters.Length == 0) { throw new ArgumentException("Invalid parameters"); }

            IList<Car> data = new List<Car>();
            var dir = "";
            if (parameters.Length == 2)
            {
                dir = parameters[1];
            }
            if (parameters[0].ToLower() == "sold")
            {
                data = Service.GetCars(true, dir);
            }
            else if (parameters[0].ToLower() == "active") { data = Service.GetCars(false, dir); }
            else if (parameters[0].ToLower() == "all") { data = Service.GetCars(dir); }
            else { throw new ArgumentException("Invalid parameters!"); }

            var result = data.Select(c => new CarVM
            {
                Id = c.Id,
                BrandName = c.Brand.Name,
                Model = c.Model,
                EngineCap = c.EngineCapacity,
                HorsePower = c.HorsePower,
                ProductionDate = c.ProductionDate,
                Price = c.Price,
                BodyType = c.BodyType.Name,
                NDoors = c.BodyType.NumberOfDoors,
                Color = c.Color.Name,
                ColorType = c.Color.ColorType.Name,
                Fuel = c.FuelType.Name,
                Gearbox = c.GearBox.GearType.Name,
                NumberOfGears = c.GearBox.NumberOfGears,
                Extras = c.CarsExtras.Select(ce => ce.Extra.Name).ToList()
            });

            return string.Join($"{new string('-', 151)}\r\n", result);
        }
    }
}
