﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Application.Utils {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Application.Utils.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Genre with this name already exitsts.
        /// </summary>
        internal static string ExistingGenreError {
            get {
                return ResourceManager.GetString("ExistingGenreError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User with this login already exists.
        /// </summary>
        internal static string ExistingUserLoginError {
            get {
                return ResourceManager.GetString("ExistingUserLoginError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Genre with this name not found.
        /// </summary>
        internal static string GenreNameNotFound {
            get {
                return ResourceManager.GetString("GenreNameNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Jwt settings not found.
        /// </summary>
        internal static string JwtSettingsError {
            get {
                return ResourceManager.GetString("JwtSettingsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Entity not found.
        /// </summary>
        internal static string NotFoundError {
            get {
                return ResourceManager.GetString("NotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User with this id not found.
        /// </summary>
        internal static string UserIdNotFound {
            get {
                return ResourceManager.GetString("UserIdNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wrong password.
        /// </summary>
        internal static string WrongPasswordError {
            get {
                return ResourceManager.GetString("WrongPasswordError", resourceCulture);
            }
        }
    }
}
