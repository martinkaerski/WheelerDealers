﻿using Dealership.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dealership.Web.Models
{
    public class CarViewModel
    {
        public CarViewModel()
        {
        }

        public CarViewModel(Car car)
        {
            this.Id = car.Id;
            this.CarModel = car.CarModel.Name;
            this.HorsePower = car.HorsePower;
            this.Mileage = car.Mileage;
            this.EngineCapacity = car.EngineCapacity;
            this.Price = car.Price;
            this.BodyType = car.BodyType.Name;
            this.Brand = car.Brand.Name;
            this.Color = car.Color.Name;
            this.ColorType = car.Color.ColorType.Name;
            this.ProductionDate = car.ProductionDate;
            this.GearBoxType = car.GearBox.GearType.Name;
            this.NumberOfGears = car.GearBox.NumberOfGears;
            this.FuelType = car.FuelType.Name;
            this.ImageUrl = car.ImageName;
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        [MinLength(2)]
        public string CarModel { get; set; }
        public int CarModelId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public short HorsePower { get; set; }

        [Required]
        [Range(1, 100000)]
        public short EngineCapacity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProductionDate { get; set; }

        public int BrandId { get; set; }
        public string Brand { get; set; }

        public int BodyTypeId { get; set; }
        public string BodyType { get; set; }


        public string Color { get; set; }

        public int ColorTypeId { get; set; }
        public string ColorType { get; set; }

        public int FuelTypeId { get; set; }
        public string FuelType { get; set; }

        public int GearBoxTypeId { get; set; }
        public string GearBoxType { get; set; }
        public byte NumberOfGears { get; set; }

        public int Mileage { get; set; }

        public IEnumerable<string> CarsExtras { get; set; }

        public IFormFile Image { get; set; }

        public string ImageUrl { get; set; }

    }
}
