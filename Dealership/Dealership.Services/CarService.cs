﻿using Dealership.Data.Models;
using Dealership.Data.UnitOfWork;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dealership.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Car CreateCar(string brandName, string model, short horsePower, short engineCapacity,
            DateTime productionDate, decimal price, string chassisName, string colorName, string colorType,
            string fuelTypeName, string gearboxTypeName, int numOfGears)
        {
            var brand = this.unitOfWork.GetRepository<Brand>().All().FirstOrDefault(b => b.Name == brandName);

            if (brand == null)
            {
                brand = new Brand() { Name = brandName };
                this.unitOfWork.GetRepository<Brand>().Add(brand);
                this.unitOfWork.SaveChanges();
            }

            var chassis = this.unitOfWork.GetRepository<Chassis>().All().FirstOrDefault(c => c.Name == chassisName);
            if (chassis == null) { throw new ChassisNotFoundException($"There is no chassis with name \"{chassisName}\"."); }

            var color = this.unitOfWork.GetRepository<Color>().All().FirstOrDefault(c => c.Name == colorName);
            if (color == null)
            {
                color = new Color
                {
                    Name = colorName,
                    ColorType = this.unitOfWork.GetRepository<ColorType>().All()
                                               .FirstOrDefault(ct => ct.Name == colorType)
                };

                if (color.ColorType == null) { throw new ColorTypeNotFoundException($"There is no color type with name \"{chassisName}\"."); }
                this.unitOfWork.GetRepository<Color>().Add(color);
                this.unitOfWork.SaveChanges();
            }

            var fuelType = this.unitOfWork.GetRepository<FuelType>().All()
                                          .FirstOrDefault(f => f.Name == fuelTypeName);
            if (fuelType == null) { throw new FuelNotFoundException($"There is no fuel with name \"{fuelTypeName}\"."); }

            var gearbox = this.unitOfWork.GetRepository<Gearbox>().All()
                .FirstOrDefault(g => g.GearType.Name == gearboxTypeName
                                  && g.NumberOfGears == numOfGears);
            if (gearbox == null) { throw new GearboxNotFoundException($"There is no such a gearbox."); }

            var newCar = new Car()
            {
                BrandId = brand.Id,
                Brand = brand,
                Model = model,
                HorsePower = horsePower,
                EngineCapacity = engineCapacity,
                ProductionDate = productionDate,
                Price = price,
                Chasis = chassis,
                ChasisId = chassis.Id,
                Color = color,
                ColorId = color.Id,
                FuelType = fuelType,
                FuelTypeId = fuelType.Id,
                GearBox = gearbox,
                GearBoxId = gearbox.Id
            };

            return newCar;
        }

        public void AddCar(Car car)
        {
            this.unitOfWork.GetRepository<Car>().Add(car);
            this.unitOfWork.SaveChanges();
        }

        public void AddCars(ICollection<Car> cars)
        {
            foreach (var car in cars)
            {
                this.unitOfWork.GetRepository<Car>().Add(car);
            }
            this.unitOfWork.SaveChanges();
        }

        public IList<Car> GetCars(bool filterSold, string direction)
        {
            var querry = this.unitOfWork.GetRepository<Car>().All()
                                            .Where(c => c.IsSold == filterSold)
                                            .Include(c => c.Brand)
                                            .Include(c => c.CarsExtras)
                                                 .ThenInclude(ce => ce.Extra)
                                            .Include(c => c.Chasis)
                                            .Include(c => c.Color)
                                                .ThenInclude(co => co.ColorType)
                                            .Include(c => c.FuelType)
                                            .Include(c => c.GearBox)
                                                .ThenInclude(gb => gb.GearType);

            if (direction.ToLower() == "asc") { return querry.OrderBy(c => c.Id).ToList(); }
            else if (direction.ToLower() == "desc") { return querry.OrderByDescending(c => c.Id).ToList(); }
            else { return querry.ToList(); }
        }

        public IList<Car> GetCars(string direction)
        {
            var querry = this.unitOfWork.GetRepository<Car>().All()
                                             .Include(c => c.Brand)
                                             .Include(c => c.CarsExtras)
                                                  .ThenInclude(ce => ce.Extra)
                                             .Include(c => c.Chasis)
                                             .Include(c => c.Color)
                                                 .ThenInclude(co => co.ColorType)
                                             .Include(c => c.FuelType)
                                             .Include(c => c.GearBox)
                                                 .ThenInclude(gb => gb.GearType);

            if (direction.ToLower() == "asc") { return querry.OrderBy(c => c.Id).ToList(); }
            else if (direction.ToLower() == "desc") { return querry.OrderByDescending(c => c.Id).ToList(); }
            else { return querry.ToList(); }
        }

        public Car GetCar(int id)
        {
            var car = this.unitOfWork.GetRepository<Car>().All()
                                           .Where(c => c.Id == id)
                                           .Include(c => c.Brand)
                                           .Include(c => c.CarsExtras)
                                                .ThenInclude(ce => ce.Extra)
                                           .Include(c => c.Chasis)
                                           .Include(c => c.Color)
                                               .ThenInclude(co => co.ColorType)
                                           .Include(c => c.FuelType)
                                           .Include(c => c.GearBox)
                                               .ThenInclude(gb => gb.GearType)
                                           .FirstOrDefault();

            if (car == null) { throw new CarNotFoundException($"There is no car with ID {id}."); }
            return car;
        }

        public Car RemoveCar(int id)
        {
            var car = GetCar(id);
            this.unitOfWork.GetRepository<Car>().Delete(car);
            this.unitOfWork.SaveChanges();

            return car;
        }

        public void EditBrand(int id, string newValue) // works but must include navigation props tables !
        {
            var car = GetCar(id);
            Brand newBrand = unitOfWork.GetRepository<Brand>().All().FirstOrDefault(b => b.Name == newValue);

            if (newBrand == null) { newBrand = new Brand() { Name = newValue }; }
            car.Brand = newBrand;
            unitOfWork.SaveChanges();

        }

        public void EditModel(int id, string newValue)
        {
            var car = GetCar(id);
            car.Model = newValue;
            unitOfWork.SaveChanges();
        }

        public void EditHorsePower(int id, string newValue)
        {
            var car = GetCar(id);
            car.HorsePower = short.Parse(newValue);
            unitOfWork.SaveChanges();
        }

        public void EditEngineCapacity(int id, string newValue)
        {
            var car = GetCar(id);
            car.EngineCapacity = short.Parse(newValue);
            unitOfWork.SaveChanges();
        }

        public void EditIsSold(int id, string newValue)
        {
            var car = GetCar(id);
            car.IsSold = bool.Parse(newValue);
            unitOfWork.SaveChanges();
        }

        public void EditPrice(int id, string newValue)
        {
            var car = GetCar(id);
            car.Price = decimal.Parse(newValue);
            unitOfWork.SaveChanges();
        }

        public void EditProductionDate(int id, string newValue)
        {
            var car = GetCar(id);
            car.ProductionDate = DateTime.Parse(newValue);
            unitOfWork.SaveChanges();
        }

        public void EditChassis(int id, string newValue)
        {
            var car = GetCar(id);
            car.Chasis.Name = newValue;
            unitOfWork.SaveChanges();
        }

        public void EditColor(int id, string newValue)
        {
            var car = GetCar(id);

            var newColor = unitOfWork.GetRepository<Color>().All().FirstOrDefault(c => c.Name == newValue);

            if (newColor == null)
            {
                newColor = new Color()
                {
                    Name = newValue,
                    ColorType = new ColorType() { Name = "Metalic" } //TODO: WHY?

                };//default type "Metalic"
                unitOfWork.GetRepository<Color>().Add(newColor);
            }

            car.Color = newColor;
            unitOfWork.SaveChanges();
        }

        public void EditColorType(int id, string newValue)
        {
            var car = GetCar(id);
            var newColorType = unitOfWork.GetRepository<ColorType>().All().FirstOrDefault(ct => ct.Name == newValue);
            if (newColorType == null) { throw new ArgumentNullException("Color type not exist!"); }

            car.Color.ColorType = newColorType;
            unitOfWork.SaveChanges();
        }

        public void EditFuelType(int id, string newValue)
        {
            var car = GetCar(id);
            var newFuelType = unitOfWork.GetRepository<FuelType>().All().FirstOrDefault(ft => ft.Name == newValue);
            if (newFuelType != null)
            {
                car.FuelType = newFuelType;
                unitOfWork.SaveChanges();
            }
            else { throw new ArgumentException($"Fuel type :{newValue} not exist!"); }

        }

        public void EditGearbox(int id, string newValue) // works but must include navigation props tables !
        {
            var car = GetCar(id);
            var newGearType = unitOfWork.GetRepository<GearType>().All().FirstOrDefault(gb => gb.Name == newValue);
            if (newGearType == null) { throw new ArgumentException($"Gearbox:{newValue} not exist!"); }
            car.GearBox.GearType = newGearType;
            unitOfWork.SaveChanges();

        }

        public Brand GetBrand(string brandName)
        {
            var brand = this.unitOfWork.GetRepository<Brand>().All().FirstOrDefault(b => b.Name == brandName);
            if (brand == null) { throw new BrandNotFoundException($"There is no brand with name \"{brandName}\"."); }
            return brand;
        }
    }
}
