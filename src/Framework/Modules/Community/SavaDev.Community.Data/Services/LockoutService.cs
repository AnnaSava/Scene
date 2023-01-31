using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Services;
using SavaDev.Community.Data;
using SavaDev.Community.Data.Contract;
using SavaDev.Community.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Data.Services
{
    public class LockoutService : BaseEntityService<Lockout, LockoutModel>, ILockoutService
    {
        public LockoutService(CommunityContext dbContext, IMapper mapper)
            : base(dbContext, mapper, nameof(LockoutService))
        {

        }

        // TODO пейджинг
        public async Task<IEnumerable<string>> GetAllActualIds(Guid communityId)
        {
            var list = await _dbContext.Set<Lockout>()
                .Where(m => m.CommunityId == communityId && m.LockoutEnd > DateTime.Now)
                .Select(m => m.UserId)
                .ToListAsync();

            return list;
        }
    }
}
