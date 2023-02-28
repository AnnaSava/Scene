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
        //protected readonly ISecurityService _securityService;

        protected readonly ServiceOptions _options;

        public BaseViewService(IMapper mapper, ServiceOptions options)
        {
            _mapper = mapper;
            _options = options;
        }

        protected Dictionary<string, string> GetStrings(Type type)
        {
            try
            {
                var p = type.GetProperties();
                var p1 = p.Where(f => f.PropertyType == typeof(string));
                var p2 = p1.ToDictionary(f => f.Name, f => (string)f.GetValue(null));

                return p2;
            }
            catch(Exception ex)
            {
                var t = ex.Message;
                throw;
            }
        }

        protected OperationViewResult MakeResponseObject<TModel, TViewModel>(OperationResult resultModel)
            where TModel : BaseRestorableModel<long>
        {
            var result =_options.SilentResponse ? new OperationViewResult(resultModel.Rows)
                : new OperationViewResult(resultModel.Rows, resultModel.ProcessedObject);
            return result;
        }
    }
}
