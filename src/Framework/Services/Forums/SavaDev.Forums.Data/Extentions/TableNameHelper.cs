using Framework.Base.Types.Enums;

namespace Sava.Forums.Data
{
    internal class TableNameHelper
    {
        private NamingConvention namingConvention;
        private string tablePrefix;

        public TableNameHelper(NamingConvention namingConvention, string tablePrefix)
        {
            this.namingConvention = namingConvention;
            this.tablePrefix = tablePrefix;
        }
    }
}