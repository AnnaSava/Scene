﻿using SavaDev.Base.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Data.Entities
{
    public class Subscription : BaseCommunityEntity, IAnyEntity
    {
        public string UserId { get; set; }

        public bool IsApprovedByOwner { get; set; }

        public bool IsApprovedByUser { get; set; }

        public bool IsLocked { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsDeleted { get; set; }
    }
}
