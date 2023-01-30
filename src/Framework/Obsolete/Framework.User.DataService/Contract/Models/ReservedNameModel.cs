using Framework.Base.Types.ModelTypes;
using System.Text.RegularExpressions;

namespace Framework.User.DataService.Contract.Models
{
    public class ReservedNameModel : IAnyModel
    {
        private const string TextRegexPattern = @"^\w*$";

        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                // TODO оставить проверки здесь или перенести в маппер? при маппинге entity в модель они не нужны
                var preparedValue = value?.ToLower().Trim();
                if (string.IsNullOrEmpty(preparedValue)) return;

                var rx = new Regex(TextRegexPattern);
                if (rx.IsMatch(preparedValue)) text = preparedValue;
            }
        }

        public bool IncludePlural { get; set; }
    }
}
