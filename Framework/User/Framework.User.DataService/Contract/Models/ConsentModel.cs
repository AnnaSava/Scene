using Framework.Base.DataService.Contract.Models;
using Framework.Base.Types.ModelTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.DataService.Contract.Models
{
    public class ConsentModel : BaseModel<int>, IModel<int>
    {
        public string Text { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsApproved { get; set; }
    }
}
