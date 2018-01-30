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
    public class BusinessEntities_Should
    {
        DbContextOptions<AdventureWorks2014Context> _dbContextOptions;

        public BusinessEntities_Should()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AdventureWorks2014Context>()
                            .UseInMemoryDatabase(databaseName: "Test_database")
                            .Options;
        }

        [Fact]
        public async void GetBusinessEntity()
        {
            using (var context = new AdventureWorks2014Context(_dbContextOptions))
            {
                var businessEntitiesAPI = new BusinessEntitiesController(context);
                for (int i = 0; i < 10; ++i)
                {
                    BusinessEntity tmpBusinessEntity = new BusinessEntity();
                    tmpBusinessEntity.BusinessEntityId = i + 1;
                    businessEntitiesAPI.PostBusinessEntity(tmpBusinessEntity).Wait();
                }
            }

            using (var context = new AdventureWorks2014Context(_dbContextOptions))
            {
                var businessEntitiesAPI = new BusinessEntitiesController(context);
                var result = await businessEntitiesAPI.GetBusinessEntity(5);
                var okResult = result as OkObjectResult;

                Assert.NotNull(okResult);
                Assert.Equal(200, okResult.StatusCode);

                BusinessEntity businessEntity = okResult.Value as BusinessEntity;
                Assert.NotNull(businessEntity);
                Assert.Equal(5, businessEntity.BusinessEntityId);
            }
        }
    }
}
