using AutoMapper;
using SavaDev.Base.Data.Services;
using Sava.Articles.Data;
using Sava.Articles.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sava.Articles.Data.Services
{
    public class RubricService : BaseEntityService<Rubric, RubricModel>, IRubricService
    {
        public RubricService(ArticlesContext dbContext, IMapper mapper) : base(dbContext, mapper, nameof(RubricService))
        {

        }
    }
}
