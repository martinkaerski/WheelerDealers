﻿using Dealership.Client.Commands.Abstract;
using Dealership.Data.Models.Contracts;
using Dealership.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Client.Commands.UserCommands
{
    public class RegisterCommand : PrimeCommand
    {
        public RegisterCommand(IUserSession userSession) : base(userSession)
        {
        }

        public IUserService UserService { get; set; }

        public override string Execute(string[] parameters)
        {
            if (base.UserSession.CurrentUser != null)
            {
                throw new InvalidOperationException("There is logged in user. Logout first to register new user!");
            }

            string username = parameters[0];
            string password = parameters[1];
            string confirmPassword = parameters[2];
            string email = parameters[3];


            var user = this.UserService.RegisterUser(username, password, confirmPassword, email);

            base.UserSession.CurrentUser = user;

            return $"User {username} was registered succesfully";
        }
    }
}