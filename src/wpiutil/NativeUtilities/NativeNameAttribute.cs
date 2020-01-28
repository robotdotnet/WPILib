using System;
using System.Collections.Generic;
using System.Text;

namespace WPIUtil.NativeUtilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NativeNameAttribute : Attribute
    {
        /// <summary>
        /// Gets the native name for this field if set.
        /// </summary>
        public string? NativeName { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="NativeNameAttribute"/>,
        /// using the name of the field as the native name.
        /// </summary>
        public NativeNameAttribute()
        {
            NativeName = null;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NativeNameAttribute"/>,
        /// with the name of the native method passed in.
        /// </summary>
        /// <param name="nativeName"></param>
        public NativeNameAttribute(string nativeName)
        {
            NativeName = nativeName;
        }
    }
}
