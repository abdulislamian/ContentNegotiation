using ContentNegotiation.ContentFormater;

namespace ExtensionMethod
{
    public static class CustomFormatter
    {
        public static IMvcBuilder CustomCSVFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config => config.OutputFormatters.Add(new
            CsvOutputFormatter()));
        }
        public static IMvcBuilder CustomHtmlFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config => config.OutputFormatters.Add(new
            HtmlOutputFormatter()));
        }
        public static IMvcBuilder CustomTextFormatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config => config.OutputFormatters.Add(new
            PlainTextOutputFormatter()));
        }
    }
}
