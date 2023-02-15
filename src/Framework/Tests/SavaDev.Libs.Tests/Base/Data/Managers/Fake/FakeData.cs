using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Managers.Fake
{
    internal class FakeData
    {
        internal static IEnumerable<FakeEntity> GetNewEntities()
        {
            return new List<FakeEntity>
            {
                new FakeEntity
                {
                    Id = 1,
                    IsDeleted = false,
                },
                new FakeEntity
                {
                    Id = 2,
                    IsDeleted = false,
                },
                new FakeEntity
                {
                    Id = 3,
                    IsDeleted = false,
                },
                new FakeEntity
                {
                    Id = 4,
                    IsDeleted = false,
                },
                new FakeEntity
                {
                    Id = 5,
                    IsDeleted = false,
                },
                new FakeEntity
                {
                    Id = 6,
                    IsDeleted = false,
                },
                new FakeEntity
                {
                    Id = 7,
                    IsDeleted = false,
                },
                new FakeEntity
                {
                    Id = 8,
                    IsDeleted = false,
                },
                new FakeEntity
                {
                    Id = 9,
                    IsDeleted = true,
                },
                new FakeEntity
                {
                    Id = 10,
                    IsDeleted = true,
                },
            };
        }
    }
}
