namespace ContentNegotiation.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    namespace ContentNegotiation.Helper
    {
        public static class TextHelper
        {
            public static string ToPlainTextTable<T>(this IEnumerable<T> enums)
            {
                var type = typeof(T);
                var props = type.GetProperties();
                var maxLengths = new int[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    maxLengths[i] = props[i].Name.Length;
                }

                foreach (var e in enums)
                {
                    var values = props.Select(s => Convert.ToString(s.GetValue(e)) ?? "N/A").ToArray();
                    for (int i = 0; i < values.Length; i++)
                    {
                        maxLengths[i] = Math.Max(maxLengths[i], values[i].Length);
                    }
                }

                var buffer = new StringBuilder();

                for (int i = 0; i < props.Length; i++)
                {
                    buffer.Append(props[i].Name.PadRight(maxLengths[i] + 2));
                }
                buffer.AppendLine();

                foreach (var e in enums)
                {
                    var values = props.Select(s => Convert.ToString(s.GetValue(e)) ?? "N/A").ToArray();
                    for (int i = 0; i < values.Length; i++)
                    {
                        buffer.Append(values[i].PadRight(maxLengths[i] + 2));
                    }
                    buffer.AppendLine();
                }

                return buffer.ToString();
            }
        }
    }

}
