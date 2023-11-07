using System.Text;

namespace ContentNegotiation.Helper
{
    public static class HtmlHelper
    {
        public static string ToHtmlTable<T>(this IEnumerable<T> enums)
        {
            var type = typeof(T);
            var props = type.GetProperties();
            var buffer = new StringBuilder("<html><body><table class='table table-striped'>");

            buffer.Append("<thead><tr>");
            foreach (var p in props)
                buffer.Append("<th>" + p.Name + "</th>");
            buffer.Append("</tr></thead>");

            buffer.Append("<tbody>");
            foreach (var e in enums)
            {
                buffer.Append("<tr>");
                props.Select(s => s.GetValue(e)).ToList().ForEach(p => {
                    buffer.Append("<td>" + p + "</td>");
                });
                buffer.Append("</tr>");
            }

            buffer.Append("</tbody>");
            buffer.Append("</table></body></html>");
            return buffer.ToString();

        }
    }
}
