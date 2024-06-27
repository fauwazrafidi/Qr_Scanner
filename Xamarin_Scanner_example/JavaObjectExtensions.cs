using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using Java.Interop;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xamarin_Scanner_example
{
    public static class JavaObjectExtensions
    {
        public static T ToNetObject<T>(this Java.Lang.Object javaObject) where T : class, Android.Runtime.IJavaObject
        {
            return javaObject.Handle != IntPtr.Zero ? Java.Lang.Object.GetObject<T>(javaObject.Handle, JniHandleOwnership.DoNotTransfer) : null;
        }
    }
}