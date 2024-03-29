﻿using AutoMapper;
using SavaDev.Base.Data.Models.Interfaces;
using SavaDev.Base.Data.Registry;
using SavaDev.Base.Data.Services;
using SavaDev.Base.Front.Exceptions;
using SavaDev.Base.Front.Registry;
using SavaDev.Content.Data;
using SavaDev.Content.Data.Contract;
using SavaDev.Content.Data.Contract.Models;
using SavaDev.Content.View.Contract;
using SavaDev.Content.View.Contract.Models;
using System.Text.Json;

namespace SavaDev.Content.View.Managers
{
    [Obsolete]
    public class DraftContentManager<TForm>
        where TForm : class, IHavingDraftsFormViewModel, new()
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

        public DraftContentManager(string Entity, string Module, string UserId, IDraftService draftService, IMapper mapper)
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

                var entityForm = await GetOneForm(id.Value);

                var form = _mapper.Map<TForm>(entityForm);

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
            var filter = new DraftStrictFilterModel
            {
                ContentId = contentId,
                Entity = _entityCode,
                Module = _moduleCode,
            };
            var vm = await GetDrafts(filter, page, count);
            return vm;
        }

        public async Task<ItemsPageViewModel<DraftViewModel>> GetDrafts(string contentId, string groupingKey, int page, int count)
        {
            var filter = new DraftStrictFilterModel
            {
                ContentId = contentId,
                GroupingKey = groupingKey,
                Entity = _entityCode,
                Module = _moduleCode,
            };
            var vm = await GetDrafts(filter, page, count);
            return vm;
        }

        private async Task<ItemsPageViewModel<DraftViewModel>> GetDrafts(DraftStrictFilterModel filter, int page, int count)
        {
            if (string.IsNullOrEmpty(filter.ContentId))
                filter.ContentId = ZeroIdString;

            var canAny = await CanGetAnyDrafts(filter.ContentId);
            var canSelf = await CanGetSelfDrafts(filter.ContentId);

            if (!canAny || !canSelf)
                throw new NotPermittedException();

            filter.OwnerId = canAny ? null : _userId;

            var query = new RegistryQuery(page, count);
            query.Filter0 = filter;
            var list = await _draftService.GetAll(query);
            var vm = new ItemsPageViewModel<DraftViewModel>(_mapper.Map<IEnumerable<DraftViewModel>>(list.Items), list.Page, list.TotalPages, list.TotalRows);
            return vm;
        }

        public async Task<Guid> SaveDraft(Guid draftId, TForm model, long contentId)
        {
            if (contentId == 0 && !await CanCreate()) throw new NotPermittedException();
            if (contentId != 0 && !await CanUpdate(contentId)) throw new NotPermittedException();

            if (draftId == Guid.Empty)
            {
                var draft = new DraftModel
                {
                    Entity = _entityCode,
                    Module = _moduleCode,
                    ContentId = contentId == 0 ? null : contentId.ToString(),
                    OwnerId = _userId
                };

                var result = await _draftService.Create(draft, model);
                return result.GetProcessedObject<DraftModel>().Id;
            }

            await _draftService.Update(draftId, model);
            return draftId;
        }

        public async Task<OperationResult> DeleteDrafts<T, T2>(T model, T2 resultModel, bool setContentId = false)
            where T : IHavingDraftsModel
            where T2 : IModel<long>
        {
            // TODO подумать, что возвращать, когда операция не выполнена за отсутствием такой необходимости
            var res = new OperationResult(1);
            if (model.DraftId.HasValue && model.DraftId != Guid.Empty)
            {
                if (setContentId)
                {
                    await _draftService.SetContentId(model.DraftId.Value, resultModel.Id.ToString());
                }
                res = await _draftService.Delete(model.DraftId.Value);
            }
            return res;
        }
    }
}
