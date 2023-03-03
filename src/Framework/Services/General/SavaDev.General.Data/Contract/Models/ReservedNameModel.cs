using SavaDev.Base.Data.Models.Interfaces;
using System.Text.RegularExpressions;

namespace SavaDev.General.Data.Contract.Models
{
    public class ReservedNameModel : IAnyModel, IFormModel
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
