using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Managers.Fake
{
    internal class TestData
    {
        internal static IEnumerable<TestEntity> GetNewEntities()
        {
            return new List<TestEntity>
            {
                new TestEntity
                {
                    Id = 1,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 2,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 3,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 4,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 5,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 6,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 7,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 8,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 9,
                    IsDeleted = true,
                },
                new TestEntity
                {
                    Id = 10,
                    IsDeleted = true,
                },
            };
        }
    }
}
