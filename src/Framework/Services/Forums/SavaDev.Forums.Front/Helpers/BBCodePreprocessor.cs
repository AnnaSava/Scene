using CodeKicker.BBCode.Core;
using System.Text;
using System.Text.RegularExpressions;

namespace Sava.Forums.Helpers
{
    public static class BBCodePreprocessor
    {
        private const string SmileImgTagTemplate = "<img src=\"/smiles/forumpack/sm{0}.gif\"/>";

        public static string Process(string input)
        {
            var parser = new BBCodeParser(new[]
                {
                    new BBTag("b", "<b>", "</b>", 1),
                    new BBTag("i", "<span style=\"font-style:italic;\">", "</span>", 2),
                    new BBTag("u", "<span style=\"text-decoration:underline;\">", "</span>", 3),
                    new BBTag("code", "<pre class=\"prettyprint\">", "</pre>", 4),
                    new BBTag("img", "<img src=\"${content}\" />", "", autoRenderContent:false, requireClosingTag: true, 5),
                    new BBTag("quote", "<blockquote>", "</blockquote>", 6),
                    new BBTag("list", "<ul>", "</ul>", 7),
                    new BBTag("*", "<li>", "</li>", autoRenderContent:true, requireClosingTag: false, 8),
                    new BBTag("url", "<a href=\"${href}\">", "</a>", 9, "", false, new BBAttribute("href", ""), new BBAttribute("href", "href")),
                    new BBTag("color", "<span style=\"color:${rgb}\">", "</span>", 10, "", false, new BBAttribute("rgb",""))
                });

            input = parser.ToHtml(input);

            input = input.Replace("\n", "<br/>");

            // Smileys

            var sb = new StringBuilder(input);

            var match = Regex.Match(input, @"\[sm(\d+)\]");

            while (match.Success)
            {
                var matched_id = match.Groups[1].Value;

                var html = string.Format(SmileImgTagTemplate, matched_id);
                sb.Replace(match.ToString(), html);

                match = match.NextMatch();
            }

            return sb.ToString();
        }
    }
}
