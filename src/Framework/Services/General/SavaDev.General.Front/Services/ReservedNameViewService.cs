using AutoMapper;
using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.General.Data.Contract;
using SavaDev.General.Data.Contract.Models;
using SavaDev.General.Data.Services;
using SavaDev.General.Front.Contract;
using SavaDev.General.Front.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.General.Front.Services
{
    public class ReservedNameViewService : IReservedNameViewService
    {
        private readonly IReservedNameService _reservedNameService;
        private readonly IMapper _mapper;

        public ReservedNameViewService(IReservedNameService reservedNameDbService, IMapper mapper)
        {
            _reservedNameService = reservedNameDbService;
            _mapper = mapper;
        }

        public async Task<ReservedNameViewModel> GetOne(string text)
        {
            var entity = await _reservedNameService.GetOne(text);
            return _mapper.Map<ReservedNameViewModel>(entity);
        }

        public async Task<ReservedNameViewModel> Create(ReservedNameViewModel model)
        {
            var entity = _mapper.Map<ReservedNameModel>(model);            
            var created = await _reservedNameService.Create(entity);
            return _mapper.Map<ReservedNameViewModel>(created);
        }

        public async Task Remove(string text)
        {
            await _reservedNameService.Remove(text);
        }

        public async Task<ReservedNameViewModel> Update(string text, ReservedNameFormViewModel model)
        {
            var entity = _mapper.Map<ReservedNameModel>(model);
            entity.Text = text;
            var updated = await _reservedNameService.Update(entity);

            return _mapper.Map<ReservedNameViewModel>(updated);
        }

        public async Task<RegistryPageViewModel<ReservedNameViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<ReservedNameModel, ReservedNameFilterModel>(_reservedNameService, _mapper);
            var vm = await manager.GetRegistryPage<ReservedNameViewModel>(query);
            return vm;
        }

        public async Task<Dictionary<string, bool>> CheckExists(string text)
        {
            var dict = new Dictionary<string, bool>();

            if (!string.IsNullOrEmpty(text))
                dict.Add(nameof(text), await _reservedNameService.CheckExists(text));

            return dict;
        }

        public async Task<FieldValidationResult> ValidateField(string name, string value)
        {
            if(name == "text")
            {
                var text = value;

                if (!string.IsNullOrEmpty(text))
                {
                    var result = new FieldValidationResult(name);
                    var exists = await _reservedNameService.CheckExists(text);
                    if (exists) result.Messages.Add($"Reserved name {text} already exists");

                    result.IsValid = !exists;
                    return result;
                }
            }
            return null;
        }

        public async Task<bool> CheckIsReserved(string text) => await _reservedNameService.CheckIsReserved(text);
    }
}
