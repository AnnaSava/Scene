﻿using Savadev.Content.Data.Contract.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;

namespace Savadev.Content.Data.Contract
{
    public interface IVersionService
    {
        Task<OperationResult<VersionModel>> Create<T>(VersionModel model, T contentModel);

        Task<ItemsPage<VersionModel>> GetAll(RegistryQuery<VersionFilterModel> query);
    }
}