using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Contract.Models
{
    public class ConsentViewModel
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Text { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsApproved { get; set; }
    }
}
