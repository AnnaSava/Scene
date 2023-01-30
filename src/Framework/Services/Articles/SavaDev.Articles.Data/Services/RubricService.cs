using AutoMapper;
using Framework.Base.DataService.Services;
using Sava.Articles.Data;
using Sava.Articles.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data.Services
{
    public class RubricService : AliasedEntityService<Rubric, RubricModel>, IRubricService
    {
        public RubricService(ArticlesContext dbContext, IMapper mapper) : base(dbContext, mapper, nameof(RubricService))
        {

        }
    }
}
