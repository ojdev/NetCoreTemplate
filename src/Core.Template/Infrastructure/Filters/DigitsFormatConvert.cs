using Newtonsoft.Json;
using System;
using System.Linq;

namespace Core.Template.Infrastructure.Filters
{
    /// <summary>
    /// 小数位数格式化
    /// </summary>
    public class DigitsFormatConvert : JsonConverter
    {
        /// <summary>
        /// 保留小数位数
        /// </summary>
        public virtual int Digits { get; private set; }
        /// <summary>
        /// 小数位数格式化
        /// </summary>
        /// <param name="digits">保留小数位数</param>
        public DigitsFormatConvert(int digits = 2)
        {
            Digits = digits;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {

                if (decimal.TryParse($"{value}", out var number))
                {
                    try
                    {
                        decimal formatter = Math.Round(number, Digits);
                        writer.WriteValue(formatter);
                    }
                    catch
                    {
                        writer.WriteValue(value);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }
        /// <summary>
        /// 
        /// </summary>
        public override bool CanRead => false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double) || objectType == typeof(float) || objectType == typeof(decimal);
        }
    }
}
