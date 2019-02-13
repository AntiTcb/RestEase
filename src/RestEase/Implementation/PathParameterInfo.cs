using System;
using System.Collections.Generic;
using System.Linq;

namespace RestEase.Implementation
{
    /// <summary>
    /// Class containing information about a desired path parameter
    /// </summary>
    public abstract class PathParameterInfo
    {
        /// <summary>
        /// Gets a value indicating whether this path parameter should be URL-encoded
        /// </summary>
        public bool UrlEncode { get; protected set; }

        /// <summary>
        /// Serialize the value into a collection of name -> value pairs using its ToString method
        /// </summary>
        /// <param name="formatProvider"><see cref="IFormatProvider"/> to use if the value implements <see cref="IFormattable"/></param>
        /// <returns>Serialized value</returns>
        public abstract KeyValuePair<string, string> SerializeToString(IFormatProvider formatProvider);
    }

    /// <summary>
    /// Class containing information about a desired path parameter
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    public class PathParameterInfo<T> : PathParameterInfo
    {
        private readonly string name;
        private readonly T value;
        private readonly string format;

        /// <summary>
        /// Initialises a new instance of the <see cref="PathParameterInfo"/> Structure
        /// </summary>
        /// <param name="name">Name of the name/value pair</param>
        /// <param name="value">Value of the name/value pair</param>
        /// <param name="format">Format parameter to pass to ToString if value implements <see cref="IFormattable"/></param>
        /// <param name="urlEncode">Indicates whether this parameter should be url-encoded</param>
        public PathParameterInfo(string name, T value, string format, bool urlEncode)
        {
            this.name = name;
            this.value = value;
            this.format = format;
            this.UrlEncode = urlEncode;
        }

        /// <summary>
        /// Serialize the value into a collection of name -> value pairs using its ToString method
        /// </summary>
        /// <param name="formatProvider"><see cref="IFormatProvider"/> to use if the value implements <see cref="IFormattable"/></param>
        /// <returns>Serialized value</returns>
        public override KeyValuePair<string, string> SerializeToString(IFormatProvider formatProvider)
        {
            return new KeyValuePair<string, string>(this.name, ToStringHelper.ToString(this.value, this.format, formatProvider));
        }
    }

    /// <summary>
    /// Class containing information about a collection of desired query parameters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PathCollectionParameterInfo<T> : PathParameterInfo
    {
        private readonly string name;
        private readonly IEnumerable<T> values;
        private readonly string format;
        private readonly string separator;

        /// <summary>
        /// Initialises a new instance of the <see cref="PathParameterInfo"/> Structure
        /// </summary>
        /// <param name="name">Name of the name/value pair</param>
        /// <param name="values">Values of the name/value pair</param>
        /// <param name="separator">Separator for values collection</param>
        /// <param name="format">Format parameter to pass to ToString if value implements <see cref="IFormattable"/></param>
        /// <param name="urlEncode">Indicates whether this parameter should be url-encoded</param>
        public PathCollectionParameterInfo(string name, IEnumerable<T> values, string separator, string format, bool urlEncode)
        {
            this.name = name;
            this.values = values;
            this.separator = separator;
            this.format = format;
            this.UrlEncode = UrlEncode;
        }

        /// <summary>
        /// Serialize the value into a collection of name -> value pairs using its ToString method
        /// </summary>
        /// <param name="formatProvider"><see cref="IFormatProvider"/> to use if the value implements <see cref="IFormattable"/></param>
        /// <returns>Serialized value</returns>
        public override KeyValuePair<string, string> SerializeToString(IFormatProvider formatProvider)
        {
            if (this.values == null)
                throw new ArgumentNullException(nameof(this.values));

            return new KeyValuePair<string, string>(this.name, string.Join(this.separator, this.values.Select(value => ToStringHelper.ToString(value, this.format, formatProvider))));
        }
    }
}
