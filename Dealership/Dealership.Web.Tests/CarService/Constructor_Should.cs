﻿using Dealership.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealership.Web.Tests.CarService
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenNullArgumentIsPassed()
        {
            //arrange
            //act&assert
            Assert.ThrowsException<ArgumentNullException>(() => new Services.CarService(null));
        }
        [TestMethod]
        public void NotThrowException_WhenValidUnitOfWorkIsPassed()
        {
            //arrange 
            var contexOptions = new DbContextOptionsBuilder<DealershipContext>()
                .UseInMemoryDatabase(databaseName: "AddCarToDatabase_WhenValidParametersArePassed").Options;

            //act
            using (var dealershipContext = new DealershipContext(contexOptions))
            {
                var carService = new Services.CarService(dealershipContext);
            }
        }
    }
}
