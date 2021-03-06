﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManahostManager.Utils.MailingUtils.Resources {
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
    public class MailTemplate {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MailTemplate() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ManahostManager.Utils.MailingUtils.Resources.MailTemplate", typeof(MailTemplate).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome {0} {1} {2},&lt;br/&gt;&lt;br/&gt;Thank you for your registration on Manahost.&lt;br/&gt;One more step is needed for activating your account. You just need to go &lt;a href=&quot;{3}&quot;&gt;there&lt;/a&gt;&lt;br/&gt;Once done, for logging in use these informations :&lt;br/&gt;&lt;br/&gt;&lt;strong&gt;Email :&lt;/strong&gt; {4}&lt;br/&gt;&lt;strong&gt;Password :&lt;/strong&gt; The password you specified during the subscription. If you forgot it, please go &lt;a href=&quot;{5}&quot;&gt;there&lt;/a&gt;&lt;br/&gt;&lt;br/&gt;.
        /// </summary>
        public static string BodyAccountCreation {
            get {
                return ResourceManager.GetString("BodyAccountCreation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello {0} {1} {2},&lt;br/&gt;&lt;br/&gt;&lt;strong&gt;The Manahost teams has detected a lot of failed connection on your account.&lt;/strong&gt;&lt;br/&gt;If those are not from you please think about changing your password&lt;br/&gt;&lt;br/&gt;If you lost your password please, go &lt;a href=&quot;{3}&quot;&gt;there&lt;/a&gt;.&lt;br/&gt;&lt;br/&gt;.
        /// </summary>
        public static string BodyConnectionWarning {
            get {
                return ResourceManager.GetString("BodyConnectionWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello {0} {1} {2},&lt;br/&gt;&lt;br/&gt;You asked for retrieving your password on Manahost.&lt;br/&gt;Please go &lt;a href=\&quot;{3}\&quot;&gt;there&lt;/a&gt; and follow each steps.&lt;br/&gt;&lt;br/&gt;.
        /// </summary>
        public static string BodyResetPassword {
            get {
                return ResourceManager.GetString("BodyResetPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to If you received this email by mistake, please contact us at &lt;a href=&apos;mailto:contact@manahost.fr&apos;&gt;contact@manahost.fr&lt;/a&gt;&lt;br/&gt;&lt;br/&gt;
        ///Thanks,&lt;br/&gt;&lt;a href=&quot;http://manahost.fr&quot;&gt;The Manahost Team&lt;/a&gt;.
        /// </summary>
        public static string Footer {
            get {
                return ResourceManager.GetString("Footer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to Manahost.
        /// </summary>
        public static string SubjectAccountCreation {
            get {
                return ResourceManager.GetString("SubjectAccountCreation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Someone is trying to use your account.
        /// </summary>
        public static string SubjectConnectionWarning {
            get {
                return ResourceManager.GetString("SubjectConnectionWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Retrieve your password.
        /// </summary>
        public static string SubjectResetPassword {
            get {
                return ResourceManager.GetString("SubjectResetPassword", resourceCulture);
            }
        }
    }
}
