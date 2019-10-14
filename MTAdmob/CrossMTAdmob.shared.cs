using System;
using MarcTron.Plugin.Interfaces;
// ReSharper disable InconsistentNaming

namespace MarcTron.Plugin
{
    /// <summary>
    /// Cross MTAdmob
    /// </summary>
    public static class CrossMTAdmob
    {
        static readonly Lazy<IMTAdmob> Implementation = new Lazy<IMTAdmob>(CreateMTAdmob, System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        public static bool IsSupported => Implementation.Value != null;

        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        public static IMTAdmob Current
        {
            get
            {
                IMTAdmob ret = Implementation.Value;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }

                return ret;
            }
        }

        static IMTAdmob CreateMTAdmob()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            return null;
#else
#pragma warning disable IDE0022 // Use expression body for methods
            return new MTAdmobImplementation();
#pragma warning restore IDE0022 // Use expression body for methods
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException(
                "This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
}