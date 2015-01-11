using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCOM.Serializer.Attributes
{
    /// <summary>
    /// Sets when to ignore the field/property
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class JIgnoreAttribute : Attribute
    {
        /// <summary>
        /// Sets when to ignore.
        /// </summary>
        /// <param name="ignoreMode">In what mode to ignore the value</param>
        public JIgnoreAttribute(JCOMIgnoreMode ignoreMode = JCOMIgnoreMode.NeverIncluded)
        {
            IgnoreMode = ignoreMode;
        }

        /// <summary>
        /// The ignore mode to assign to the property or field.
        /// </summary>
        public JCOMIgnoreMode IgnoreMode { get; set; }

        /// <summary>
        /// Returns true if the ignore mode dose not include always.
        /// </summary>
        public bool IsIncluded { get { return (IgnoreMode & JCOMIgnoreMode.NeverIncluded) != JCOMIgnoreMode.NeverIncluded; } }
    }

    [Flags]
    public enum JCOMIgnoreMode { IfNull = 1, IfDefualt = 2, NeverIncluded = 4, NeverIgnored = 8 }
}
