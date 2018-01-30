using DALlab4.Entities;
using Lab4.ApiContollers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test.UnitTests
{
    public class Stores_Should
    {
        DbContextOptions<AdventureWorks2014Context> _dbContextOptions;

        public Stores_Should()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AdventureWorks2014Context>()
                            .UseInMemoryDatabase(databaseName: "Test_database")
                            .Options;
        }

        [Fact]
        public async void GetStore()
        {
            using (var context = new AdventureWorks2014Context(_dbContextOptions))
            {
                var storesAPI = new StoresController(context);
                for (int i = 0; i < 10; ++i)
                {
                    Store tmpStore = new Store();
                    tmpStore.Name = $"Store { i + 1 }";
                    storesAPI.PostStore(tmpStore).Wait();
                }
            }

            using (var context = new AdventureWorks2014Context(_dbContextOptions))
            {
                var storesAPI = new StoresController(context);
                var result = await storesAPI.GetStore(5);
                var okResult = result as OkObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);

                Store store = okResult.Value as Store;
                Assert.NotNull(store);
                Assert.Equal("Store 5", store.Name);
            }
        }
    }
}
