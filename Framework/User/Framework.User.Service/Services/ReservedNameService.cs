using AutoMapper;
using Framework.Base.DataService.Contract.Models;
using Framework.Base.Service.ListView;
using Framework.Base.Types.Validation;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Contract.Models;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Contract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service.Services
{
    public class ReservedNameService : IReservedNameService
    {
        private readonly IReservedNameDbService _reservedNameDbService;
        private readonly IMapper _mapper;

        public ReservedNameService(IReservedNameDbService reservedNameDbService, IMapper mapper)
        {
            _reservedNameDbService = reservedNameDbService;
            _mapper = mapper;
        }

        public async Task<ReservedNameViewModel> GetOne(string text)
        {
            var entity = await _reservedNameDbService.GetOne(text);
            return _mapper.Map<ReservedNameViewModel>(entity);
        }

        public async Task<ReservedNameViewModel> Create(ReservedNameViewModel model)
        {
            var entity = _mapper.Map<ReservedNameModel>(model);            
            var created = await _reservedNameDbService.Create(entity);
            return _mapper.Map<ReservedNameViewModel>(created);
        }

        public async Task Remove(string text)
        {
            await _reservedNameDbService.Remove(text);
        }

        public async Task<ReservedNameViewModel> Update(string text, ReservedNameFormViewModel model)
        {
            var entity = _mapper.Map<ReservedNameModel>(model);
            entity.Text = text;
            var updated = await _reservedNameDbService.Update(entity);

            return _mapper.Map<ReservedNameViewModel>(updated);
        }

        public async Task<ListPageViewModel<ReservedNameViewModel>> GetAll(ReservedNameFilterViewModel filter, ListPageInfoViewModel pageInfo)
        {
            var filterModel = _mapper.Map<ReservedNameFilterModel>(filter);

            var pageInfoModel = _mapper.Map<PageInfoModel>(pageInfo);

            var list = await _reservedNameDbService.GetAll(new ListQueryModel<ReservedNameFilterModel> { Filter = filterModel, PageInfo = pageInfoModel });

            var vm = new ListPageViewModel<ReservedNameViewModel>()
            {
                Items = list.Items.Select(m => _mapper.Map<ReservedNameModel, ReservedNameViewModel>(m)),
                Page = list.Page,
                TotalPages = list.TotalPages,
                TotalRows = list.TotalRows
            };

            return vm;
        }

        public async Task<Dictionary<string, bool>> CheckExists(string text)
        {
            var dict = new Dictionary<string, bool>();

            if (!string.IsNullOrEmpty(text))
                dict.Add(nameof(text), await _reservedNameDbService.CheckExists(text));

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
                    var exists = await _reservedNameDbService.CheckExists(text);
                    if (exists) result.Messages.Add($"Reserved name {text} already exists");

                    result.IsValid = !exists;
                    return result;
                }
            }
            return null;
        }

        public async Task<bool> CheckIsReserved(string text) => await _reservedNameDbService.CheckIsReserved(text);
    }
}
