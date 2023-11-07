using ContentNegotiation.Helper.ContentNegotiation.Helper;
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
            var data = context.Object as IEnumerable<StudentDTO>;

            if (data == null)
                return;

            using (var buffer = new MemoryStream())
            using (var writer = new StreamWriter(buffer, selectedEncoding))
            {
                var plainTextTable = data.ToPlainTextTable(); 

                writer.WriteLine(plainTextTable);

                writer.Flush();

                buffer.Position = 0;
                await buffer.CopyToAsync(response.Body);
            }

        }
    }

}
