using System;

[assembly:global::Android.Runtime.NamespaceMapping (Java = "android.device", Managed="Android.Device")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "android.device.scanner.configuration", Managed="Android.Device.Scanner.Configuration")]
[assembly:global::Android.Runtime.NamespaceMapping (Java = "android.device.sdk", Managed="Android.Device.Sdk")]

delegate int _JniMarshal_PP_I (IntPtr jnienv, IntPtr klass);
delegate IntPtr _JniMarshal_PP_L (IntPtr jnienv, IntPtr klass);
delegate bool _JniMarshal_PP_Z (IntPtr jnienv, IntPtr klass);
delegate int _JniMarshal_PPI_I (IntPtr jnienv, IntPtr klass, int p0);
delegate IntPtr _JniMarshal_PPI_L (IntPtr jnienv, IntPtr klass, int p0);
delegate void _JniMarshal_PPI_V (IntPtr jnienv, IntPtr klass, int p0);
delegate bool _JniMarshal_PPI_Z (IntPtr jnienv, IntPtr klass, int p0);
delegate bool _JniMarshal_PPII_Z (IntPtr jnienv, IntPtr klass, int p0, int p1);
delegate bool _JniMarshal_PPIL_Z (IntPtr jnienv, IntPtr klass, int p0, IntPtr p1);
delegate IntPtr _JniMarshal_PPL_L (IntPtr jnienv, IntPtr klass, IntPtr p0);
delegate void _JniMarshal_PPL_V (IntPtr jnienv, IntPtr klass, IntPtr p0);
delegate bool _JniMarshal_PPL_Z (IntPtr jnienv, IntPtr klass, IntPtr p0);
delegate IntPtr _JniMarshal_PPLII_L (IntPtr jnienv, IntPtr klass, IntPtr p0, int p1, int p2);
delegate int _JniMarshal_PPLL_I (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1);
delegate void _JniMarshal_PPLL_V (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1);
delegate bool _JniMarshal_PPLL_Z (IntPtr jnienv, IntPtr klass, IntPtr p0, IntPtr p1);
delegate void _JniMarshal_PPLZ_V (IntPtr jnienv, IntPtr klass, IntPtr p0, bool p1);
delegate void _JniMarshal_PPZ_V (IntPtr jnienv, IntPtr klass, bool p0);
#if !NET
namespace System.Runtime.Versioning {
    [System.Diagnostics.Conditional("NEVER")]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Enum | AttributeTargets.Event | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Module | AttributeTargets.Property | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
    internal sealed class SupportedOSPlatformAttribute : Attribute {
        public SupportedOSPlatformAttribute (string platformName) { }
    }
}
#endif

