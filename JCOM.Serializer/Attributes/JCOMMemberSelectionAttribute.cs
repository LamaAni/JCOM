using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCOM.Serializer.Attributes
{
    internal interface IMembersSelectionAttribute
    {
        JCOMMemberSelectionType Selection { get; set; }
    }

    /// <summary>
    /// Determines which members to select, if the OptIn is selected then only specifically defined
    /// members are added. Dose not affect the base class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class JCOMInheritedMemberSelectionAttribute : Attribute, IMembersSelectionAttribute
    {
        /// <summary>
        /// Set the member selection type of the current, if the OptIn is selected then only specifically defined
        /// members are added. DOSE NOT AFFECT THE BASE CLASS!
        /// </summary>
        /// <param name="selection"></param>
        public JCOMInheritedMemberSelectionAttribute(JCOMMemberSelectionType selection = JCOMMemberSelectionType.Properties)
        {
            Selection = selection;
        }

        /// <summary>
        /// The selection type for the current members.
        /// </summary>
        public JCOMMemberSelectionType Selection { get; set; }
    }

    /// <summary>
    /// Set the member selection type of the current, if the OptIn is selected then only specifically defined
    /// members are added. Affectes only the declared class (is not inherited by derived classes). Trumps any decleration
    /// of JCOMMemberSelectionAttribute. Use JCOMInheritedMemberSelectionAttribute for inherited member selection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class JCOMMemberSelectionAttribute : Attribute, IMembersSelectionAttribute
    {
        /// <summary>
        /// Set the member selection type of the current, if the OptIn is selected then only specifically defined
        /// members are added. Affectes only the declared class (is not inherited by derived classes). Trumps any decleration
        /// of JCOMMemberSelectionAttribute.
        /// </summary>
        /// <param name="selection"></param>
        public JCOMMemberSelectionAttribute(JCOMMemberSelectionType selection = JCOMMemberSelectionType.Properties)
        {
            Selection = selection;
        }

        /// <summary>
        /// The selection type for the current members.
        /// </summary>
        public JCOMMemberSelectionType Selection { get; set; }
    }

    /// <summary>
    /// Determiens which members to select.
    /// </summary>
    [Flags]
    public enum JCOMMemberSelectionType { Fields = 1, Properties = 2, ReadOnlyProperties = 4, OptIn = 8 }
}
