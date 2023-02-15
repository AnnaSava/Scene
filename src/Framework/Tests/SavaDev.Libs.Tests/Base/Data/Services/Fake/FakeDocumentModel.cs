using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Libs.Tests.Base.Data.Services.Fake
{
    public class FakeDocumentModel : BaseDocumentFormModel<long>, IModel<long>
    {
    }
}
