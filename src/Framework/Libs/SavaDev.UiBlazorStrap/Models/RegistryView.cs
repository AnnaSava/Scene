using System.Collections.Generic;

namespace SavaDev.UiBlazorStrap.Models
{
    public class RegistryView<T> where T: RegistryViewItem
    {
        public List<T> Items { get; set; } = new List<T>();
    }
}
