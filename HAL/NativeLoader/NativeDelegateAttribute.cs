using System;

namespace HAL.NativeLoader
{
    /// <summary>
    /// Specifies that the attributed field should be considered a target for native initialization 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class NativeDelegateAttribute : Attribute
    {
        /// <summary>
        /// Gets the native name for this field if set.
        /// </summary>
        public string NativeName { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="NativeDelegateAttribute"/>, 
        /// using the name of the field as the native name.
        /// </summary>
        public NativeDelegateAttribute()
        {
            NativeName = null;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NativeDelegateAttribute"/>,
        /// with the name of the native method passed in.
        /// </summary>
        /// <param name="nativeName"></param>
        public NativeDelegateAttribute(string nativeName)
        {
            NativeName = nativeName;
        }
    }
}
