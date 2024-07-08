package crc64238089920ed48059;


public class ProjectAutoCompleteAdapter
	extends android.widget.ArrayAdapter
	implements
		mono.android.IGCUserPeer,
		android.widget.Filterable
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_getView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"n_getFilter:()Landroid/widget/Filter;:GetGetFilterHandler\n" +
			"";
		mono.android.Runtime.register ("Xamarin_Scanner_example.ProjectAutoCompleteAdapter, Xamarin_Scanner_example", ProjectAutoCompleteAdapter.class, __md_methods);
	}


	public ProjectAutoCompleteAdapter (android.content.Context p0, int p1)
	{
		super (p0, p1);
		if (getClass () == ProjectAutoCompleteAdapter.class) {
			mono.android.TypeManager.Activate ("Xamarin_Scanner_example.ProjectAutoCompleteAdapter, Xamarin_Scanner_example", "Android.Content.Context, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public ProjectAutoCompleteAdapter (android.content.Context p0, int p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ProjectAutoCompleteAdapter.class) {
			mono.android.TypeManager.Activate ("Xamarin_Scanner_example.ProjectAutoCompleteAdapter, Xamarin_Scanner_example", "Android.Content.Context, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public ProjectAutoCompleteAdapter (android.content.Context p0, int p1, java.lang.Object[] p2)
	{
		super (p0, p1, p2);
		if (getClass () == ProjectAutoCompleteAdapter.class) {
			mono.android.TypeManager.Activate ("Xamarin_Scanner_example.ProjectAutoCompleteAdapter, Xamarin_Scanner_example", "Android.Content.Context, Mono.Android:System.Int32, mscorlib:T[], Mono.Android", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public ProjectAutoCompleteAdapter (android.content.Context p0, int p1, int p2, java.lang.Object[] p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ProjectAutoCompleteAdapter.class) {
			mono.android.TypeManager.Activate ("Xamarin_Scanner_example.ProjectAutoCompleteAdapter, Xamarin_Scanner_example", "Android.Content.Context, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib:T[], Mono.Android", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public ProjectAutoCompleteAdapter (android.content.Context p0, int p1, java.util.List p2)
	{
		super (p0, p1, p2);
		if (getClass () == ProjectAutoCompleteAdapter.class) {
			mono.android.TypeManager.Activate ("Xamarin_Scanner_example.ProjectAutoCompleteAdapter, Xamarin_Scanner_example", "Android.Content.Context, Mono.Android:System.Int32, mscorlib:System.Collections.Generic.IList`1<T>, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public ProjectAutoCompleteAdapter (android.content.Context p0, int p1, int p2, java.util.List p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ProjectAutoCompleteAdapter.class) {
			mono.android.TypeManager.Activate ("Xamarin_Scanner_example.ProjectAutoCompleteAdapter, Xamarin_Scanner_example", "Android.Content.Context, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib:System.Collections.Generic.IList`1<T>, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public android.view.View getView (int p0, android.view.View p1, android.view.ViewGroup p2)
	{
		return n_getView (p0, p1, p2);
	}

	private native android.view.View n_getView (int p0, android.view.View p1, android.view.ViewGroup p2);


	public android.widget.Filter getFilter ()
	{
		return n_getFilter ();
	}

	private native android.widget.Filter n_getFilter ();

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
