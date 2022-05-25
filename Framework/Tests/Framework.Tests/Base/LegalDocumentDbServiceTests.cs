using AutoMapper;
using Framework.Tests.Base.Data;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Mapper;
using Framework.User.DataService.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.Base
{
    internal class LegalDocumentDbServiceTests : IDisposable
    {
        private IMapper _mapper;
        private LegalDocumentTestDbContext _context;
        private LegalDocumentDbService _permissionDbService;

        public LegalDocumentDbServiceTests()
        {
            _mapper = new MapperConfiguration(opts => { opts.AddProfile<CommonDataMapperProfile>(); }).CreateMapper();
            _context = GetContext();
            _permissionDbService = new LegalDocumentDbService(_context, _mapper);
            FillContextWithTestData(_context, TestData.GetLegalDocuments());
        }

        public void Dispose()
        {
            _mapper = null;
            _context = null;
            _permissionDbService = null;
        }

        private LegalDocumentTestDbContext GetContext()
        {
            var options = Infrastructure.GetOptionsAction();

            var optionsBuilder = new DbContextOptionsBuilder<LegalDocumentTestDbContext>();
            options.Invoke(optionsBuilder);

            return new LegalDocumentTestDbContext(optionsBuilder.Options);
        }

        private void FillContextWithTestData(LegalDocumentTestDbContext context, IEnumerable<LegalDocument> data)
        {
            context.Database.EnsureCreated();
            context.LegalDocuments.AddRange(data);
            context.SaveChanges();
        }
    }
}
