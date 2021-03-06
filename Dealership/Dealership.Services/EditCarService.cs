﻿using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Dealership.Services
{
    public class EditCarService : IEditCarService
    {
        private readonly ICarService carService;
        private readonly DealershipContext context;

        public EditCarService(DealershipContext context, ICarService carService)
        {
            if (carService == null)
            {
                throw new ArgumentNullException("CarService cannot be null!");
            }
            this.context = context;
            this.carService = carService;
        }

        public virtual string EditBrand(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            int id;
            if (!int.TryParse(parameters[0], out id))
            {
                throw new ArgumentException("Invalid ID!");
            }

            string newValue = parameters[1];

            string secondNewValue = "";
            if (parameters.Length == 3)
            {
                secondNewValue = parameters[2];
            }

            var car = this.carService.GetCar(id);

            Brand newBrand = this.context.Brands.FirstOrDefault(b => b.Name == newValue);

            if (newBrand == null)
            {
                newBrand = new Brand() { Name = newValue };
            }
            car.Brand = newBrand;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Brand of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";

        }

        public string EditModel(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }
            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);

            var model = car.Brand.CarModels.FirstOrDefault(m => m.Name == newValue);
            if (model == null)
            {
                model = new CarModel() { Name = newValue, BrandId = car.Brand.Id };
                this.context.CarModels.Add(model);
              //  this.context.SaveChanges(); //TODO:TEST
            }
            //id??
            car.CarModel = model;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Model of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public string EditHorsePower(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.HorsePower = short.Parse(newValue);

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Horse power of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public string EditEngineCapacity(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }

            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.EngineCapacity = short.Parse(newValue);

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Engine capacity of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public string EditIsSold(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }

            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.IsSold = bool.Parse(newValue);

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"IsSold of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public string EditPrice(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.Price = decimal.Parse(newValue);

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Price of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public string EditMileAge(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }

            if (!int.TryParse(parameters[1], out int newValue))
            {
                throw new ArgumentException("Invalid mileage value!");
            }
            var car = this.carService.GetCar(id);
            car.Mileage = newValue;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Mileage of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public string EditProductionDate(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            car.ProductionDate = DateTime.Parse(newValue);

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Production date of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public string EditBodyType(string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                throw new ArgumentNullException("Invalid amount of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var newBodyType = this.context.BodyTypes.FirstOrDefault(ch => ch.Name == newValue);

            if (newBodyType == null)
            {
                throw new ArgumentException("Invalid body type!");
            }

            var car = this.carService.GetCar(id);
            car.BodyType = newBodyType;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Body type of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public string EditColor(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newColorValue = parameters[1];
            string newColorTypeName = "";

            if (parameters.Length == 3)
            {
                newColorTypeName = parameters[2];
            }
            var car = this.carService.GetCar(id);

            var newType = this.context.ColorTypes.FirstOrDefault(gt => gt.Name == newColorTypeName);
            if (newType == null)
            {
                throw new ArgumentException("Invalid color type!");
            }

            var newColor = this.context.Colors
                                     .Include(c => c.ColorType)
                                     .FirstOrDefault(c => c.Name == newColorValue
                                     && c.ColorType.Name == newColorTypeName);

            if (newColor == null)
            {
                newColor = new Color() { Name = newColorValue, ColorType = newType };

                this.context.Colors.Add(newColor);
                this.context.SaveChanges();
            }

            car.ColorId = newColor.Id;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Color of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public string EditColorType(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);
            var colorName = car.Color.Name;
            var newColorType = this.context.ColorTypes.FirstOrDefault(ct => ct.Name == newValue);
            if (newColorType == null) { throw new ArgumentNullException("Color type not exist!"); }

            var newColor = this.context.Colors
                .Include(c => c.ColorType)
                .FirstOrDefault(c => c.Name == colorName
                && c.ColorType.Name == newValue);
            if (newColor == null)
            {
                newColor = new Color { Name = colorName, ColorType = newColorType };
                this.context.Colors.Add(newColor);
                this.context.SaveChanges();
            }

            car.Color = newColor;
            car.ColorId = newColor.Id;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Color type of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }

        public string EditFuelType(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);

            var newFuelType = this.context.FuelTypes.FirstOrDefault(ft => ft.Name == newValue);

            if (newFuelType != null)
            {
                car.FuelType = newFuelType;
                this.context.Cars.Update(car);
                this.context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Fuel type :{newValue} not exist!");
            }

            return $"Fuel type of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";

        }

        public string EditGearbox(string[] parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("Parameters cannot be null!");
            }
            if (parameters.Length == 0)
            {
                throw new ArgumentException("Invalid number of parameters!");
            }

            if (!int.TryParse(parameters[0], out int id))
            {
                throw new ArgumentException("Invalid ID!");
            }
            var newValue = parameters[1];

            var car = this.carService.GetCar(id);

            GearType newGearType = this.context.GearTypes.First(gt => gt.Name == newValue);

            if (newGearType == null)
            {
                throw new ArgumentException($"Gearbox:{newValue} not exist!");
            }
            car.GearBox.GearType = newGearType;

            this.context.Cars.Update(car);
            this.context.SaveChanges();

            return $"Gearbox of {car.Brand.Name} {car.CarModel.Name} with ID:{car.Id} edited successfully!";
        }
    }
}
