﻿using SavaDev.Base.Data.Services;
using SavaDev.Community.Data.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Community.Data.Contract
{
    public interface ISubscriptionService
    {
        Task<OperationResult> Create(SubscriptionModel model);

        Task Delete(SubscriptionModel model);

        Task<bool> IsSubscribed(SubscriptionModel model);

        Task<IEnumerable<Guid>> GetAllActiveSubscriptions(SubscriptionModel model, string entityName, string module);

        Task<IEnumerable<string>> GetAllActualSubscriberIds(Guid communityId);

        Task<IEnumerable<string>> GetAllRequestedSubscriberIds(Guid communityId);

        Task<IEnumerable<string>> GetAllInvitedSubscriberIds(Guid communityId);
    }
}
