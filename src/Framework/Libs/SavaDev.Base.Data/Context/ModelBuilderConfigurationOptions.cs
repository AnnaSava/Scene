using SavaDev.Base.Data.Enums;

namespace SavaDev.Base.Data.Context
{
    public class ModelBuilderConfigurationOptions
    {
        public string TablePrefix { get; set; }

        public NamingConvention NamingConvention { get; set; }

        public ModelBuilderConfigurationOptions()
        {
        }
    }
}
