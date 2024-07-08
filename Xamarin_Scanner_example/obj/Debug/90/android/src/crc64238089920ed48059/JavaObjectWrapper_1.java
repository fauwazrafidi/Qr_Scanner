package crc64238089920ed48059;


public class JavaObjectWrapper_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Xamarin_Scanner_example.JavaObjectWrapper`1, Xamarin_Scanner_example", JavaObjectWrapper_1.class, __md_methods);
	}


	public JavaObjectWrapper_1 ()
	{
		super ();
		if (getClass () == JavaObjectWrapper_1.class) {
			mono.android.TypeManager.Activate ("Xamarin_Scanner_example.JavaObjectWrapper`1, Xamarin_Scanner_example", "", this, new java.lang.Object[] {  });
		}
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
