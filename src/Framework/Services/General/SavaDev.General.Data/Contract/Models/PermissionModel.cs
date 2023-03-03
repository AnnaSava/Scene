using SavaDev.Base.Data.Models.Interfaces;

namespace SavaDev.General.Data.Contract.Models
{
    public class PermissionModel : IAnyModel, IFormModel
    {
        public string Name { get; set; }
    }
}
