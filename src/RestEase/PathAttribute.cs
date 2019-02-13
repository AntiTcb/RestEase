﻿using System;

namespace RestEase
{
    /// <summary>
    /// Marks a parameter as able to substitute a placeholder in this method's path
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class PathAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the optional name of the placeholder. Will use the parameter name if null
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the format string to pass to the value's ToString() method, if the value implements <see cref="IFormattable"/>
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this path parameter should be URL-encoded. Defaults to true.
        /// </summary>
        public bool UrlEncode { get; set; } = true;


        /// <summary>
        /// Gets or sets a string for separating values for a path collection
        /// </summary>
        public string Separator { get; set; } = ",";

        /// <summary>
        /// Initialises a new instance of the <see cref="PathAttribute"/> class
        /// </summary>
        public PathAttribute()
        { }

        /// <summary>
        /// Initialises a new instance of the <see cref="PathAttribute"/> class, with the given name
        /// </summary>
        /// <param name="name">Placeholder in the path to replace</param>
        public PathAttribute(string name)
        {
            this.Name = name;
        }
    }
}
