﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Resources.Controllers
{
    using System;


    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class PersonsController
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal PersonsController()
        {
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Web.Resources.Controllers.PersonsController", typeof(PersonsController).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Bad Length.
        /// </summary>
        public static string BadLength
        {
            get
            {
                return ResourceManager.GetString("BadLength", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Duplicate USA.
        /// </summary>
        public static string Duplicate
        {
            get
            {
                return ResourceManager.GetString("Duplicate", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Geo Or Latin.
        /// </summary>
        public static string GeoOrLatin
        {
            get
            {
                return ResourceManager.GetString("GeoOrLatin", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Invalid Birth Date 18+.
        /// </summary>
        public static string InvalidBirthDate
        {
            get
            {
                return ResourceManager.GetString("InvalidBirthDate", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Value must be between {1} and {2}.
        /// </summary>
        public static string Range
        {
            get
            {
                return ResourceManager.GetString("Range", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Required.
        /// </summary>
        public static string Required
        {
            get
            {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
    }
}
