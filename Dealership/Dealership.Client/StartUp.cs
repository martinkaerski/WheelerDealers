﻿using Autofac;
using Dealership.Client.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dealership.Client
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Supported commands:");

            Console.WriteLine("register {username} {password} {confirmPass} {email}");
            Console.WriteLine("login {username} {password}");
            Console.WriteLine("logout");
            Console.WriteLine("deleteUser {username} {password}");
            Console.WriteLine();
            
            Console.WriteLine("addcar {brand} {model} {horse power} {engine capacity} {production date} {price} {chasis} {color} {color type} {fuel type} {gearbox} {number of gears}");
            Console.WriteLine("removecar {carId}");
            Console.WriteLine("createextra {extra name}");
            Console.WriteLine("addextratocar {carId, extra name}");
            Console.WriteLine("getextrasforcar {carId}");
            Console.WriteLine("list active/sold/all asc/desc ");
            Console.WriteLine("filterbybrand {brandName} / filterbyyears {yearFrom} {yearTo}");
            Console.WriteLine("filterByPrice {priceFrom} {priceTo} / filterByBodyType {bodyType}");
            
            Console.WriteLine("viewcardetails {carId}");
            Console.WriteLine("edit [exact property] [id] [new value]");
            Console.WriteLine("export sold? asc/desc");
            Console.WriteLine("import {filename} -ex: import cars");
            Console.WriteLine("generatePdf - output folder: DataProcessor/PdfReports");

            Console.WriteLine("addCarToFavorites {carId}");
            Console.WriteLine("removeCarFromFavorites {carId}";
            Console.WriteLine("listFavorites");
                       
            Console.WriteLine();
            Console.WriteLine();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var engine = container.Resolve<IEngine>();
            engine.Run();
        }
    }
}
