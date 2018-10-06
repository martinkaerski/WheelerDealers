﻿using Autofac;
using Dealership.Client.Core.Abstract;
using System;
using System.Reflection;

namespace Dealership.Client
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Supported commands:");
            Console.WriteLine("add {brand} {model} {horse power} {engine capacity} {production date} {price} {chasis} {color} {color type} {fuel type} {gearbox} {number of gears}");
            Console.WriteLine("remove {carId}");
            Console.WriteLine("list  - list ASC/DESC and list sold ASC/DESC");
            Console.WriteLine("filter  - not supported yet");
            Console.WriteLine("view  - not supported yet");
            Console.WriteLine("edit  - not supported yet");
            Console.WriteLine("export - not supported yet");
            Console.WriteLine("import - not supported yet");

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
