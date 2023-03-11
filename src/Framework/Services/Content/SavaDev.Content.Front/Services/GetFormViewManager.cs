using AutoMapper;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Front.Exceptions;
using SavaDev.Base.Front.Registry;
using SavaDev.Content.Contract.Models;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Contract.Models;
using SavaDev.Content.Front.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SavaDev.Content.Services
{
    public class GetFormViewManager<TForm>
        where TForm: class, IHavingDraftsFormViewModel, new()
    {
        protected const string ZeroIdString = "0";

        private readonly IDraftService _draftService;
        private readonly IMapper _mapper;
        private readonly string _entityCode;
        private readonly string _moduleCode;
        private readonly string _userId;

        public Func<Task<bool>>? CanCreate { get; set; }
        public Func<string, Task<bool>>? CanGetAnyDrafts { get; set; }
        public Func<string, Task<bool>>? CanGetSelfDrafts { get; set; }
        public Func<long, Task<bool>>? CanUpdate { get; set; }
        public Func<DraftModel?, Task<bool>>? CanCreateFromDraft { get; set; }
        public Func<long?, Task<object>>? GetOneForm { get; set; }

        public GetFormViewManager(string Entity, string Module, string UserId, IDraftService draftService, IMapper mapper)
        {
            _entityCode = Entity;
            _moduleCode = Module;
            _userId = UserId;
            _draftService = draftService;
            _mapper = mapper;
        }

        public async Task<TForm> GetForm(long? id, Guid? draftId)
        {
            if (ContentHelper.IdHasValue(draftId))
            {
                var draft = await _draftService.GetOne(draftId.Value);
                if (draft != null)
                {
                    if (!ContentHelper.IdEqualsContentId(id, draft.ContentId)) throw new ArgumentException();

                    // Update from draft
                    if (ContentHelper.IdHasValue(id))
                    {
                        var canUpdate = await CanUpdate(id.Value);
                        if (!canUpdate) throw new NotPermittedException();

                        var form = JsonSerializer.Deserialize<TForm>(draft.Content);
                        return form;
                    }
                    // Create from draft
                    else
                    {
                        var canCreateFromDraft = await CanCreateFromDraft(draft);
                        if (canCreateFromDraft)
                        {
                            var form = JsonSerializer.Deserialize<TForm>(draft.Content);
                            return form;
                        }
                        else
                        {
                            return new TForm();
                        }
                    }
                }
            }

            // Update
            if (ContentHelper.IdHasValue(id))
            {
                if (!await CanUpdate(id.Value)) throw new NotPermittedException();

                var goal = await GetOneForm(id.Value);

                var form = _mapper.Map<TForm>(goal);

                var drafts = await GetDrafts(id.ToString(), 1, 30);
                form.Drafts = drafts?.Items.ToList();
                form.HasMoreDrafts = drafts?.TotalRows > drafts?.Items?.Count();

                return form;
            }

            if (!await CanCreate()) throw new NotPermittedException();

            // Create
            return new TForm();
        }

        public async Task<ItemsPageViewModel<DraftViewModel>> GetDrafts(string contentId, int page, int count)
        {
            if (string.IsNullOrEmpty(contentId))
                contentId = ZeroIdString;

            //var canAny = goalId == ZeroIdString ? await HasPermission(PlannerAction.Goal.CreateAny) : await HasPermission(PlannerAction.Goal.UpdateAny);
            //var canSelf = goalId == ZeroIdString ? await HasPermission(PlannerAction.Goal.CreateSelf) : await HasPermission(PlannerAction.Goal.UpdateSelf);

            var canAny = await CanGetAnyDrafts(contentId);
            var canSelf = await CanGetSelfDrafts(contentId);

            if (!canAny || !canSelf)
                throw new NotPermittedException();

            var filter = new DraftStrictFilterModel
            {
                ContentId = contentId,
                Entity = _entityCode,
                Module = _moduleCode,
                OwnerId = canAny ? null : _userId
            };

            var query = new RegistryQuery(page, count);
            query.Filter0 = filter;
            var list = await _draftService.GetAll(query);
            var vm = new ItemsPageViewModel<DraftViewModel>(_mapper.Map<IEnumerable<DraftViewModel>>(list.Items), list.Page, list.TotalPages, list.TotalRows);
            return vm;
        }
    }
}
