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
        public static Java.Lang.Object ToJavaObject(this CompanyCode companyCode)
        {
            return new JavaObjectWrapper<CompanyCode>(companyCode);
        }
    }

    public class JavaObjectWrapper<T> : Java.Lang.Object
    {
        private readonly T _instance;

        public JavaObjectWrapper(T instance)
        {
            _instance = instance;
        }

        public T GetInstance()
        {
            return _instance;
        }
    }
}