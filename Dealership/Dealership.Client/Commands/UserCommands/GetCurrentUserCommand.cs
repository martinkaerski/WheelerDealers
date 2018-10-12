﻿using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.UserCommands
{
    public class GetCurrentUserCommand : PrimeCommand
    {
        public GetCurrentUserCommand(IUserSession userSession) : base(userSession)
        {
        }

        public IUserService UserService { get; set; }

        public override string Execute(string[] parameters)
        {
            var user = base.UserSession.CurrentUser;

            return $"Current user: {user.Username} {user.Password}";
        }
    }
}
