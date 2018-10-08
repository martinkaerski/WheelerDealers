﻿using Autofac;
using Dealership.Client.Contracts;
using Dealership.Client.Contracts.Abstract;
using Dealership.Client.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dealership.Client.Core
{
    public class DealershipEngine : IEngine
    {
        private IComponentContext containerContext;
        private readonly IReader reader;
        private readonly IWriter writer;
        
        public DealershipEngine(IComponentContext containerContext, IReader reader, IWriter writer)
        {
            this.containerContext = containerContext;
            this.reader = reader;
            this.writer = writer;
        }

        string input = string.Empty;

        public void Run()
        {

            while ((input = reader.ReadLine()) != "exit")
            {
                try
                {
                    var inputParams = input.Split();
                    var command = this.ParseCommand(inputParams[0]);
                    //commands not implemented yet !
                    //commandResult returns message according to operation result (successful or not)
                    var commandResult = command.Execute(inputParams.Skip(1).ToArray());

                    writer.WriteLine(commandResult);
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }               
            }
        }

        private ICommand ParseCommand(string commandStr)
        {
            return this.containerContext.ResolveNamed<ICommand>(commandStr);
        }
    }
}
