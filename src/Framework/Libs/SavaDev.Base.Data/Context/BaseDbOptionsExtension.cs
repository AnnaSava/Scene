using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Data.Context
{
    public class BaseDbOptionsExtension : IDbContextOptionsExtension
    {
        public string TablePrefix { get; set; }

        public NamingConvention NamingConvention { get; set; }

        public void Validate(IDbContextOptions options) { }

        public void ApplyServices(IServiceCollection services) { }

        public virtual DbContextOptionsExtensionInfo Info => new MyDbContextOptionsExtensionInfo(this);

        public class MyDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
        {
            public MyDbContextOptionsExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
            {
            }

            public override int GetServiceProviderHashCode() => 0;

            public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => true;

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {

            }

            public override bool IsDatabaseProvider => false;

            public override string LogFragment => nameof(BaseDbOptionsExtension);
        }
    }
}
