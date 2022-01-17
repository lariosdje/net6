using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net6.Api.DataContext;
using Net6.Api.Helpers;
using Net6.Api.Repository;
using System;
using System.Threading.Tasks;

namespace Net6.Test.ActivityTest
{
    [TestClass]
    public class ActivityTest
    {
        private Net6Context context;
        private IMapper mapper;
        private readonly ActivityRepository controller;
        readonly InitData data;

        public ActivityTest()
        {
            context = new Net6Context(
                new DbContextOptionsBuilder<Net6Context>()
                .UseInMemoryDatabase(databaseName: "dbTest")
                .Options
            );

            mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            }).CreateMapper();

            controller = new ActivityRepository(context, mapper);

            var data = new InitData(context);
            data.CreateData().Wait();
        }

        [TestMethod]
        public async Task CancelaActividad()
        {
            var response = await controller.CancelActivity(1);

            Assert.IsTrue(response.IsCorrect);
        }

        [TestMethod]
        public async Task ListadoActividad()
        {           
            var response = await controller.ActivityList(null, null, String.Empty);

            Assert.IsTrue(response.IsCorrect);      
        }

        
    }
}
