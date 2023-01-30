using AutoMapper;
using Framework.Base.DataService.Services;
using Framework.Community.Data.Contract;
using Framework.Community.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Community.Data.Services
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
