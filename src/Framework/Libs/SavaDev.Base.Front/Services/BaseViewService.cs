using AutoMapper;
using SavaDev.Base.Data.Models;
using SavaDev.Base.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Base.Front.Services
{
    public abstract class BaseViewService
    {
        protected const string ZeroIdString = "0";

        protected readonly IMapper _mapper;
        // TODO скорее всего перенести сюда
        //protected readonly ICurrentUserService _currentUserService;
        //protected readonly ISecurityService _securityService;

        protected readonly ServiceOptions _options;

        public BaseViewService(IMapper mapper, ServiceOptions options)
        {
            _mapper = mapper;
            _options = options;
        }

        protected Dictionary<string, string> GetStrings(Type type)
        {
            var p = type.GetProperties();
            var p1 = p.Where(f => f.PropertyType == typeof(string));
            var p2 = p1.ToDictionary(f => f.Name, f => (string)f.GetValue(null));

            return p2;
        }

        protected OperationResult<TViewModel> MakeResponseObject<TModel, TViewModel>(OperationResult<TModel> resultModel)
            where TModel : BaseRestorableModel<long>
        {
            // TODO возвращать айдишник
            return _options.SilentResponse
                  ? null// new OperationResult<TViewModel>(resultModel.Rows, resultModel.Models.FirstOrDefault().Id.ToString())
                  : new OperationResult<TViewModel>(resultModel.Rows, _mapper.Map<TViewModel>(resultModel.Models.FirstOrDefault()));
        }
    }
}
