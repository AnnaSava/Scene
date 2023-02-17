using AutoMapper;
using Microsoft.Extensions.Logging;
using SavaDev.Base.Data.Managers;
using SavaDev.Base.Data.Services;
using SavaDev.System.Data.Contract.Context;
using SavaDev.System.Data.Contract.Models;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Services.Fake
{
    public class FakeDocumentService : BaseDocumentEntityService<FakeDocument, FakeDocumentModel>
    {
        public FakeDocumentService(FakeDocumentContext dbContext,
            IEnumerable<string> availableCultures,
            IMapper mapper,
            ILogger<FakeDocumentService> logger)
            : base(dbContext, availableCultures, mapper, logger, nameof(FakeDocumentService))
        {
            
        }
    }
}
