using SavaDev.Base.Data.Models.Interfaces;

namespace SavaDev.System.Data.Contract.Models
{
    public class PermissionModel : IAnyModel, IFormModel
    {
        public string Name { get; set; }
    }
}
