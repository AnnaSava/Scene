using AutoMapper;
using Framework.Base.DataService.Contract.Interfaces;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.DataService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.Base.Data
{
    public class TestDbContext : DbContext, IDbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {

        }

        public DbSet<TestEntity> TestEntities { get; set; }
    }

    public class TestEntity : IEntity<long>, IEntityRestorable
    {
        public long Id { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class TestModel 
    {
        public long Id { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class TestFilterModel : ListFilterModel
    {

    }

    public class TestAutoMapperProfile : Profile
    {
        public TestAutoMapperProfile()
        {
            CreateMap<TestEntity, TestModel>();
            CreateMap<TestModel, TestEntity>();
        }
    }
}
