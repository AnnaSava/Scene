using Framework.Base.Exceptions;
using Framework.Base.Types.ModelTypes;

namespace Framework.Base.DataService.Services
{
    internal static class ValidationExtentions
    {
        public static TModel ValidateNotNull<TModel>(this TModel model)
            where TModel : IAnyModel
        {
            if (model == null)
                throw new ProjectArgumentException(
                    typeof(ValidationExtentions),
                    nameof(ValidateNotNull),
                    nameof(model),
                    null);
            return model;
        }

        public static TModel ValidateAlias<TModel>(this TModel model)
            where TModel : IModelAliased
        {
            if (string.IsNullOrWhiteSpace(model.Alias))
                throw new ProjectArgumentException(
                    typeof(ValidationExtentions),
                    nameof(ValidateAlias),
                    nameof(model.Alias),
                    model.Alias);
            return model;
        }
    }
}
