using SavaDev.Base.Data.Entities.Interfaces;
using SavaDev.Base.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Scheme.Front.Contract.Models
{
    public class RegistryViewModel : IModel<Guid>
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Module { get; set; }

        public string Entity { get; set; }

        public List<ColumnViewModel> DisplayedColumns = new List<ColumnViewModel>();

        public List<ColumnViewModel> AvailableColumns = new List<ColumnViewModel>();

        public List<RegistryConfigViewModel> Configs = new List<RegistryConfigViewModel>();

        public RegistryConfigViewModel? SelectedConfig { get; set; }

        public List<FilterViewModel> Filters { get; set; } = new List<FilterViewModel> { };

        public FilterViewModel? SelectedFilter { get; set; }
    }
}
