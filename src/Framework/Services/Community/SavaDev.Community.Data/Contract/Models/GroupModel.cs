﻿using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Data.Contract.Models
{
    public class GroupModel : IFormModel, IAnyModel
    {
        public Guid Id { get; set; }

        public string OwnerId { get; set; }

        public string AttachedToId { get; set; }

        public string Module { get; set; }

        public string Entity { get; set; }
    }
}
