using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavaDev.Content.Data.Contract
{
    public static class ContentHelper
    {
        public static bool IdEqualsContentId(long? id, string? contentId)
        {
            var convertedId = id.HasValue
                ? id.Value == 0
                    ? string.Empty
                    : id.Value.ToString()
                : string.Empty;

            var convertedContentId = contentId != null
                ? contentId == "0"
                    ? string.Empty
                    : contentId
                : string.Empty;

            return convertedId == convertedContentId;
        }

        public static bool IdHasValue(long? id)
        {
            return id.HasValue && id.Value != 0;
        }

        public static bool IdHasValue(Guid? id)
        {
            return id.HasValue && id.Value != Guid.Empty;
        }

        public static bool IdHasValue(string? id)
        {
            return !(string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id) || id == "0" || id == Guid.Empty.ToString());
        }
    }
}
