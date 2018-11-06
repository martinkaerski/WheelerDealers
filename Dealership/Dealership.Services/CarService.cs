﻿using Dealership.Data.Context;
using Dealership.Data.Models;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using Dealership.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dealership.Services
{
    public class CarService : ICarService
    {
        private readonly DealershipContext context;

        public CarService(DealershipContext context)
        {
            this.context = context;
        }

        //from form with ddl values
        public Car CreateCar(int brandId, int carModelId, int mileage, short horsePower,
            short engineCapacity, DateTime productionDate, decimal price, int bodyTypeId,
            string colorName, int colorTypeId, int fuelTypeId, int gearBoxTypeId, byte numberOfGears)
        {

            var color = this.context.Colors
                                                    .Include(c => c.ColorType)
                                                    .FirstOrDefault(c => c.Name == colorName
                                                     && c.ColorTypeId == colorTypeId);

            var colorTypeFromDatabase = this.context.ColorTypes
                                       .FirstOrDefault(ct => ct.Id == colorTypeId);

            if (color == null)
            {
                color = new Color { Name = colorName, ColorType = colorTypeFromDatabase };
                this.context.Colors.Add(color);
                this.context.SaveChanges();
            }

            var fuelType = this.context.FuelTypes
                                          .FirstOrDefault(f => f.Id == fuelTypeId);
            

            var gearbox = this.context.Gearboxes
                .FirstOrDefault(g => g.GearType.Id == gearBoxTypeId
                                  && g.NumberOfGears == numberOfGears);
         
            if (mileage < 0)
            {
                throw new ServiceException($"Mileage must be greater than 0.");
            }
            var newCar = new Car()
            {
                BrandId = brandId,
                CarModelId = carModelId,
                HorsePower = horsePower,
                EngineCapacity = engineCapacity,
                ProductionDate = productionDate,
                Price = price,
                Mileage = mileage,
                BodyTypeId = bodyTypeId,
                Color = color,
                ColorId = color.Id,
                FuelTypeId = fuelTypeId,
                GearBox = gearbox,
                GearBoxId = gearbox.Id
            };

            return newCar;
        }

        public Car CreateCar(string brandName, string carModelName, int mileage, short horsePower, short engineCapacity,
            DateTime productionDate, decimal price, string bodyTypeName, string colorName, string colorType,
            string fuelTypeName, string gearboxTypeName, int numOfGears)
        {
            if (brandName.Length < 2 || brandName.Length > 25)
            {
                throw new ServiceException("The name of brand cannot be less than 2 symbols or more than 25 symbols.");
            }

            var brand = this.context.Brands.Include(b => b.CarModels).FirstOrDefault(b => b.Name == brandName);

            if (brand == null)
            {
                brand = new Brand() { Name = brandName };
                this.context.Brands.Add(brand);
                this.context.SaveChanges();
            }


            var model = brand.CarModels.FirstOrDefault(m => m.Name == carModelName);
            if (model == null)
            {
                model = new CarModel() { Name = carModelName, BrandId = brand.Id };
                this.context.CarModels.Add(model);
                this.context.SaveChanges();
            }



            var bodyType = this.context.BodyTypes.FirstOrDefault(c => c.Name == bodyTypeName);
            if (bodyType == null)
            {
                throw new ServiceException($"There is no body type with name {bodyTypeName}.");
            }

            var color = this.context.Colors
                                                              .Include(c => c.ColorType)
                                                              .FirstOrDefault(c => c.Name == colorName
                                                               && c.ColorType.Name == colorType);

            var colorTypeFromDatabase = this.context.ColorTypes
                                       .FirstOrDefault(ct => ct.Name == colorType);
            if (colorTypeFromDatabase == null)
            {
                throw new InvalidOperationException($"There is no color type with name {colorType}.");
            }

            if (color == null)
            {
                color = new Color { Name = colorName, ColorType = colorTypeFromDatabase };
                this.context.Colors.Add(color);
                this.context.SaveChanges();
            }

            var fuelType = this.context.FuelTypes
                                          .FirstOrDefault(f => f.Name == fuelTypeName);
            if (fuelType == null)
            {
                throw new ServiceException($"There is no fuel with name {fuelTypeName}.");
            }

            var gearbox = this.context.Gearboxes
                .FirstOrDefault(g => g.GearType.Name == gearboxTypeName
                                  && g.NumberOfGears == numOfGears);
            if (gearbox == null)
            {
                throw new ServiceException($"There is no such a gearbox.");
            }

            if (mileage < 0)
            {
                throw new ServiceException($"Mileage must be greater than 0.");
            }
            var newCar = new Car()
            {
                BrandId = brand.Id,
                Brand = brand,
                CarModel = model,
                HorsePower = horsePower,
                EngineCapacity = engineCapacity,
                ProductionDate = productionDate,
                Price = price,
                Mileage = mileage,
                BodyType = bodyType,
                BodyTypeId = bodyType.Id,
                Color = color,
                ColorId = color.Id,
                FuelType = fuelType,
                FuelTypeId = fuelType.Id,
                GearBox = gearbox,
                GearBoxId = gearbox.Id
            };

            return newCar;
        }

        public Car AddCar(Car car)
        {
            if (car == null)
            {
                throw new ServiceException("Car doesn't exist!");
            }
            car = this.context.Cars.Add(car).Entity;
            this.context.SaveChanges();

            return car;
        }

        public void AddCars(ICollection<Car> cars)
        {
            foreach (var car in cars)
            {
                if (car != null)

                {
                    this.context.Cars.Add(car);
                }
            }
            this.context.SaveChanges();
        }

        public IList<Car> GetCars(bool filterSold, string order)
        {
            var querry = this.context.Cars
                                            .Where(c => c.IsSold == filterSold)
                                            .Include(c => c.Brand)
                                            .Include(c => c.CarsExtras)
                                                 .ThenInclude(ce => ce.Extra)
                                            .Include(c => c.BodyType)
                                            .Include(c => c.Color)
                                                .ThenInclude(co => co.ColorType)
                                            .Include(c => c.FuelType)
                                            .Include(c => c.GearBox)
                                                .ThenInclude(gb => gb.GearType);

            if (order.ToLower() == "asc")
            {
                return querry.OrderBy(c => c.Id).ToList();
            }

            else if (order.ToLower() == "desc")
            {
                return querry.OrderByDescending(c => c.Id).ToList();
            }
            else
            {
                return querry.ToList();
            }
        }

        public IList<Car> GetCars(string direction)
        {
            var querry = this.context.Cars
                                             .Include(c => c.Brand)
                                             .Include(c => c.CarsExtras)
                                                  .ThenInclude(ce => ce.Extra)
                                             .Include(c => c.BodyType)
                                             .Include(c => c.Color)
                                                 .ThenInclude(co => co.ColorType)
                                             .Include(c => c.FuelType)
                                             .Include(c => c.GearBox)
                                                 .ThenInclude(gb => gb.GearType);

            if (direction.ToLower() == "desc")
            {
                return querry.OrderByDescending(c => c.Id).ToList();
            }
            else
            {
                return querry.OrderBy(c => c.Id).ToList();
            }
        }

        public IList<Car> GetCars(int skip, int take)
        {
            var querry = this.context.Cars
                                            .Skip(skip)
                                            .Take(take)
                                            .Include(c => c.Brand)
                                            .Include(c => c.CarsExtras)
                                                 .ThenInclude(ce => ce.Extra)
                                            .Include(c => c.BodyType)
                                            .Include(c => c.Color)
                                                .ThenInclude(co => co.ColorType)
                                            .Include(c => c.FuelType)
                                            .Include(c => c.GearBox)
                                                .ThenInclude(gb => gb.GearType)
                                            ;

            return querry.ToList();
        }

        public virtual Car GetCar(int id)
        {
            var car = this.context.Cars
                                  .Where(c => c.Id == id)
                                  .Include(c => c.Brand)
                                  .Include(c=>c.CarModel)
                                  .Include(c => c.CarsExtras)
                                       .ThenInclude(ce => ce.Extra)
                                  .Include(c => c.BodyType)
                                  .Include(c => c.Color)
                                      .ThenInclude(co => co.ColorType)
                                  .Include(c => c.FuelType)
                                  .Include(c => c.GearBox)
                                      .ThenInclude(gb => gb.GearType)
                                  .FirstOrDefault();

            if (car == null)
            {
                throw new ServiceException($"There is no car with ID {id}.");
            }
            return car;
        }

        public Car RemoveCar(int id)
        {
            var car = GetCar(id);
            this.context.Cars.Remove(car);

            var usersCars = this.context.UsersCars.Where(uc => uc.CarId == id).ToList();

            foreach (var userCar in usersCars)
            {
                this.context.UsersCars.Remove(userCar);
            }

            var carsExtras = this.context.CarsExtras.Where(uc => uc.CarId == id).ToList();

            foreach (var carExtra in carsExtras)
            {
                this.context.CarsExtras.Remove(carExtra);
            }

            this.context.SaveChanges();
            return car;
        }

        public int GetCarsCount()
        {
            return this.context.Cars.Count();
        }

        public void Update(Car car)
        {
            this.context.Cars.Update(car as Car);
            this.context.SaveChanges();
        }
        public void SaveAvatarImage(string root, string filename, Stream stream, int carId)
        {
            var car = GetCar(carId);

            if (car == null)
            {
                throw new InvalidOperationException("Car not found");
            }

            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(filename);
            var path = Path.Combine(root, imageName);

            using (var fileStream = File.Create(path))
            {
                stream.CopyTo(fileStream);
            }

            car.ImageName = imageName;
            this.context.SaveChanges();
        }


    }
}

