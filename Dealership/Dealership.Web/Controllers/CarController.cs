﻿using Dealership.Data.Models;
using Dealership.Services.Abstract;
using Dealership.Web.Models;
using Dealership.Web.Models.CarViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace Dealership.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly IEditCarService editCarService;
        private readonly IBrandService brandService;
        private readonly IBodyTypeService bodyTypeService;
        private readonly IColorTypeService colorTypeService;
        private readonly IFuelTypeService fuelTypeService;
        private readonly IGearTypeService gearTypeService;
        private readonly IModelService modelService;
        private readonly IColorService colorService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public CarController(ICarService carService, IEditCarService editCarService, IBrandService brandService,
            IBodyTypeService bodyTypeService, IColorTypeService colorTypeService,
            IFuelTypeService fuelTypeService, IGearTypeService gearTypeService,
            IModelService modelService, IColorService colorService, IUserService userService, UserManager<User> userManager)
        {
            this.carService = carService;
            this.editCarService = editCarService;
            this.brandService = brandService;
            this.bodyTypeService = bodyTypeService;
            this.colorTypeService = colorTypeService;
            this.fuelTypeService = fuelTypeService;
            this.gearTypeService = gearTypeService;
            this.modelService = modelService;
            this.colorService = colorService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Index()
        {
            var list = this.carService.GetCars(0,99);

            return View(list);
           
        }

      

        [HttpGet]
        public IActionResult Browse(int page)
        {
            var nPerPage = 5;
            var pageCount = 0;
            var totalCount = this.carService.GetCarsCount();
            var reminder = totalCount % nPerPage;
            if (reminder != 0) { pageCount = (totalCount / nPerPage) + 1; }
            else { pageCount = totalCount / nPerPage; }

            //Expression<Func<Car, bool>> searchCriteria = (Car car) => true;
            //searchCriteria = x => x.Brand.Name.Contains("");

            var model = new BrowseViewModel()
            {
                Summaries = this.carService.GetCars(page * nPerPage, nPerPage)
                .Select(c => new CarSummaryViewModel(c)
                {
                    Id = c.Id,
                    Brand = c.Brand.Name,
                    CarModel = c.CarModel.Name,
                    Capacity = c.EngineCapacity,
                    GearType = c.GearBox.GearType.Name,
                    Fuel = c.FuelType.Name,
                    Color = c.Color.Name,
                    Price = $"{c.Price}$",
                    Mileage = $"{c.Mileage} miles"
                }),
                Pages = pageCount,
                CurrentPage = page
            };
            model.Brands
                .AddRange(this.brandService.GetBrands()
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList());

            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new EditCarViewModel
            {
                Brands = this.brandService.GetBrands()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                CarModels = this.modelService.GetAllModelsByBrandId(this.brandService.GetBrands().FirstOrDefault().Id)
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList(),

                NumberOfGears = this.gearTypeService.GetGearboxesDependingOnGearType(this.gearTypeService.GetGearTypes().FirstOrDefault().Id)
               .Select(x => new SelectListItem { Value = x.NumberOfGears.ToString(), Text = x.NumberOfGears.ToString() }).ToList(),

                BodyTypes = this.bodyTypeService.GetBodyTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                GearTypes = this.gearTypeService.GetGearTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                ColorTypes = this.colorTypeService.GetColorTypes()
                 .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                FuelTypes = this.fuelTypeService.GetFuelTypes()
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList(),

                Car = new CarViewModel()
                {
                    StatusMessage = this.StatusMessage
                }
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditCarViewModel model)
        {
            var car = this.carService.CreateCar(
                model.Car.BrandId, model.Car.CarModelId, model.Car.Mileage, model.Car.HorsePower,
                model.Car.EngineCapacity, model.Car.ProductionDate, model.Car.Price,
                model.Car.BodyTypeId, model.Car.Color, model.Car.ColorTypeId, model.Car.FuelTypeId,
                model.Car.GearBoxTypeId, model.Car.NumberOfGears);

            this.carService.AddCar(car);

            if (model.Images != null)
            {
                foreach (var image in model.Images)
                {
                    if (!this.IsValidImage(image))
                    {
                        this.StatusMessage = "Error: Please provide a.jpg or .png file smaller than 5MB";
                        return this.RedirectToAction(nameof(Create));
                    }
                }

                this.carService.SaveImages(
                         this.GetUploadsRoot(),
                         model.Images.Select(i => i.FileName).ToList(),
                         model.Images.Select(i => i.OpenReadStream()).ToList(),
                         car.Id
                     );
            }

            this.StatusMessage = "Car registration is successful!";
            return RedirectToAction("Details", "Car", new { id = car.Id });
        }

        public JsonResult GetModelsByBrandId(int brandId)
        {
            var list = this.modelService.GetAllModelsByBrandId(brandId);
            return Json(list);
        }

        public JsonResult GetGearsDependingOnGearBoxType(int id)
        {
            var list = (this.gearTypeService.GetGearboxesDependingOnGearType(id));
            return Json(list);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var car = this.carService.GetCar(id);
            var user = this.userManager.GetUserAsync(HttpContext.User).Result;

            var model = new CarViewModel(car)
            {
                IsFavorite = userService.IsCarFavorite(id, user),
                StatusMessage = this.StatusMessage
            };

            return this.View(model);
        }

        [Authorize]
        public IActionResult ConfirmDelete(int id)
        {
            this.carService.RemoveCar(id);

            return RedirectToAction(nameof(Browse));
        }
               
        private string GetUploadsRoot()
        {
            var environment = this.HttpContext.RequestServices
                .GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;

            return Path.Combine(environment.WebRootPath, "images", "cars");
        }

        private bool IsValidImage(IFormFile image)
        {
            string type = image.ContentType;
            if (type != "image/png" && type != "image/jpg" && type != "image/jpeg")
            {
                return false;
            }
            return image.Length <= 5242880;
        }
    }
}