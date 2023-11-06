using ContentNegotiation.Models.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace ContentNegotiation.ContentFormater
{
    public class PlainTextOutputFormatter : TextOutputFormatter
    {
        public PlainTextOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));
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
                foreach (var company in (IEnumerable<StudentDTO>)context.Object)
                {
                    FormatCsv(buffer, company);
                }
            }
            else
            {
                FormatCsv(buffer,(StudentDTO)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, StudentDTO student)
        {
            //buffer.AppendLine($"{student.Id},\"{student.Name},\"{student.Address}\"");
            buffer.AppendLine($"Id: {student.Id}");
            buffer.AppendLine($"Name: {student.Name}");
            buffer.AppendLine($"Address: {student.Address}");
            buffer.AppendLine();
        }
    }

}
