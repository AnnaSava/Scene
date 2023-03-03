using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.UiBlazorStrap
{
    public static class UiConfig
    {
#if DEBUG
        public static bool ExpandAccordion { get; } = true;
#else
        public static bool ExpandAccordion { get; } = false;
#endif
    }
}
