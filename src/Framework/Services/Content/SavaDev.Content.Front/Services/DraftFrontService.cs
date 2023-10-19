using AutoMapper;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Exceptions;
using SavaDev.Base.Front.Models;
using SavaDev.Base.Front.Registry;
using SavaDev.Base.Front.Services;
using SavaDev.Content.Contract;
using SavaDev.Content.Contract.Models;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Contract.Models;
using SavaDev.Content.Front.Contract.Enums;
using SavaDev.Content.Front.Contract.Models;

namespace SavaDev.Content.Services
{
    public class DraftFrontService : IDraftFrontService
    {
        protected const string ZeroIdString = "0";

        private readonly IDraftService _draftService;
        private readonly IMapper _mapper;

        public DraftFrontService(IDraftService draftService, IMapper mapper)
        {
            _draftService = draftService;
            _mapper = mapper;
        }

        public async Task<ServiceCheckOk> Check(ServiceCheckQuery query) => CheckFrontService.Check(query);

        public async Task<RegistryPageViewModel<DraftViewModel>> GetRegistryPage(RegistryQuery query)
        {
            var manager = new RegistryPageManager<DraftModel, DraftFilterModel>(_draftService, _mapper);
            var vm = await manager.GetRegistryPage<DraftViewModel>(query);
            return vm;
        }

        public async Task<OperationViewResult> Create(DraftViewModel model)
        {
            return new OperationViewResult(12);
        }

        public async Task<GetFormViewResult> GetForm(GetFormViewQuery query)
        {
            if (ContentHelper.IdHasValue(query.DraftId))
            {
                var draft = await _draftService.GetOne(query.DraftId.Value);
                if (draft != null)
                {
                    if (!ContentHelper.IdEqualsContentId(query.Id, draft.ContentId)) throw new ArgumentException();

                    // Update from draft
                    if (ContentHelper.IdHasValue(query.Id))
                    {
                        if (!query.CanUpdate) throw new NotPermittedException();

                        var form = new GetFormViewResult { Content = draft.Content, Id = query.Id , 
                            Action = FormActions.UpdateFromDraft};
                        return form;
                    }
                    // Create from draft
                    else
                    {
                        if (query.CanCreateFromDraft)
                        {
                            var form = new GetFormViewResult { Content = draft.Content, Action = FormActions.CreateFromDraft };
                            return form;
                        }
                        else
                        {
                            return new GetFormViewResult(); // TODO Action?
                        }
                    }
                }
            }

            // Update
            if (ContentHelper.IdHasValue(query.Id))
            {
                if (!query.CanUpdate) throw new NotPermittedException();

                var form = new GetFormViewResult() { Action = FormActions.Update, Id = query.Id };
                var drafts = await GetDrafts(query, 1, 30);
                form.Drafts = drafts?.Items.ToList();
                form.HasMoreDrafts = drafts?.TotalRows > drafts?.Items?.Count();

                return form;
            }

            if (!query.CanCreate) throw new NotPermittedException();

            // Create
            return new GetFormViewResult() { Action = FormActions.Create };
        }

        private async Task<ItemsPageViewModel<DraftViewModel>> GetDrafts(GetFormViewQuery query, int page, int count)
        {
            var filter = new DraftStrictFilterModel
            {
                ContentId = query.Id.ToString(),
                Entity = query.EntityCode,
                Module = query.ModuleCode,
            };
            var vm = await GetDrafts(filter, query, page, count);
            return vm;
        }

        private async Task<ItemsPageViewModel<DraftViewModel>> GetDrafts(DraftStrictFilterModel filter, GetFormViewQuery query1, int page, int count)
        {
            if (string.IsNullOrEmpty(filter.ContentId))
                filter.ContentId = ZeroIdString;

            var canAny = query1.CanGetAnyDrafts;
            var canSelf = query1.CanGetSelfDrafts;

            if (!canAny || !canSelf)
                throw new NotPermittedException();

            filter.OwnerId = canAny ? null : query1.UserId;

            var query = new RegistryQuery(page, count);
            query.Filter0 = filter;
            var list = await _draftService.GetAll(query);
            var vm = new ItemsPageViewModel<DraftViewModel>(_mapper.Map<IEnumerable<DraftViewModel>>(list.Items), list.Page, list.TotalPages, list.TotalRows);
            return vm;
        }
    }
}
