﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Data.Entities
{
    public abstract class BaseCommunityEntity
    {
        public long Id { get; set; }

        public Guid GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
