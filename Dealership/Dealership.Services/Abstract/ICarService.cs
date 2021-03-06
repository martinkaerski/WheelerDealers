﻿using Dealership.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dealership.Services.Abstract
{
    public interface ICarService
    {

        Car CreateCar(int brandId, int carModelId, int mileage, short horsePower, short engineCapacity,
            DateTime productionDate, decimal price, int bodyTypeId, string colorName, int colorTypeId,
            int fuelTypeId, int gearBoxTypeId, byte numberOfGears);

        Car AddCar(Car car);

        void AddCars(ICollection<Car> cars);

        Car GetCar(int id);

        IList<Car> GetCars();
        IList<Car> GetCars(int skip, int take);

        Car RemoveCar(int carId);

        int GetCarsCount();
        void Update(Car car);

        void SaveImages(string root, IList<string> fileNames, IList<Stream> stream, int carId);

    }
}