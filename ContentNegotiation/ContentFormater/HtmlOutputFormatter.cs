using ContentNegotiation.Models.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace ContentNegotiation.ContentFormater
{
    public class HtmlOutputFormatter : TextOutputFormatter
    {
        public HtmlOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/html"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type? type)
        {
            if (typeof(StudentDTO).IsAssignableFrom(type) ||
           typeof(IEnumerable<StudentDTO>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext
        context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<StudentDTO>)
            {
                buffer.Append("<html><body><table class='table table-striped'><tr><th>ID</th><th>Name</th><th>Address</th></tr>");
                foreach (var student in (IEnumerable<StudentDTO>)context.Object)
                {
                        FormatCsv(buffer, student);
                }
                buffer.Append("</table></body></html>");
            }
            else
            {
                buffer.Append("<html><body><table><tr><th>ID</th><th>Name</th><th>Address</th></tr>");
                FormatCsv(buffer,(StudentDTO)context.Object);
                buffer.Append("</table></body></html>");
            }
            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, StudentDTO student)
        {
            buffer.AppendLine($"<tr><td>{student.Id}</td><td>{student.Name}</td><td>{student.Address}</td></tr>");
        }
    }

}
