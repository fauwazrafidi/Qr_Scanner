package crc64238089920ed48059;


public class CompanyCode
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_toString:()Ljava/lang/String;:GetToStringHandler\n" +
			"";
		mono.android.Runtime.register ("Xamarin_Scanner_example.CompanyCode, Xamarin_Scanner_example", CompanyCode.class, __md_methods);
	}


	public CompanyCode ()
	{
		super ();
		if (getClass () == CompanyCode.class) {
			mono.android.TypeManager.Activate ("Xamarin_Scanner_example.CompanyCode, Xamarin_Scanner_example", "", this, new java.lang.Object[] {  });
		}
	}


	public java.lang.String toString ()
	{
		return n_toString ();
	}

	private native java.lang.String n_toString ();

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
