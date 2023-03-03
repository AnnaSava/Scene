using SavaDev.Base.Data.Registry.Filter;

namespace SavaDev.General.Data.Contract.Models
{
    public class PermissionFilterModel : BaseFilter
    {
        public WordFilterField Name { get; set; } = new();
    }
}
