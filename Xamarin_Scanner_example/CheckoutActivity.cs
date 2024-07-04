using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using com.companyname.xamarin_scanner_example;
using Java.Lang;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Android.Support.V7.Widget.RecyclerView;

namespace Xamarin_Scanner_example
{
    [Activity(Label = "Checkout")]
    public class CheckoutActivity : AppCompatActivity
    {
        //Spinner spinnerFrom, spinnerTo, spinnerProject, spinnerCompanyCode;
        AutoCompleteTextView spinnerFrom, spinnerTo, spinnerProject, spinnerCompanyCode;
        EditText editTextQty, editTextDescription;
        Button buttonSubmit;
        int DTLKEY; // Dtlkey value from DisplayDataActivity
        string ITEMCODE;
        string DESCRIPTION;
        string LOCATION;
        string PROJECT;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_checkout);

            //spinnerFrom = FindViewById<Spinner>(Resource.Id.spinnerFrom);
            //spinnerTo = FindViewById<Spinner>(Resource.Id.spinnerTo);
            //spinnerProject = FindViewById<Spinner>(Resource.Id.spinnerProject);
            //spinnerCompanyCode = FindViewById<Spinner>(Resource.Id.spinnerCompanyCode);

            spinnerFrom = FindViewById<AutoCompleteTextView>(Resource.Id.spinnerFrom);
            spinnerTo = FindViewById<AutoCompleteTextView>(Resource.Id.spinnerTo);
            spinnerProject = FindViewById<AutoCompleteTextView>(Resource.Id.spinnerProject);
            spinnerCompanyCode = FindViewById<AutoCompleteTextView>(Resource.Id.spinnerCompanyCode);

            editTextQty = FindViewById<EditText>(Resource.Id.editTextQty);
            editTextDescription = FindViewById<EditText>(Resource.Id.editTextDescription);
            buttonSubmit = FindViewById<Button>(Resource.Id.buttonSubmit);

            // Retrieve Dtlkey value from Intent
            DTLKEY = Intent.GetIntExtra("Dtlkey", 0);
            ITEMCODE = Intent.GetStringExtra("Itemcode");
            DESCRIPTION = Intent.GetStringExtra("Description");
            LOCATION = Intent.GetStringExtra("location");

            // Set default values for EditText fields
            SetDefaultValues();
            //SetUpSpinners();
            //SetUpProjectSpinners();
            //SetUpCompanyCodeSpinner();

            SetUpSearchableSpinner(spinnerFrom);
            SetUpSearchableSpinner(spinnerTo);
            SetUpProjectSpinners();
            SetUpCompanyCodeSpinner();

            buttonSubmit.Click += ButtonSubmit_Click;
        }

        private void SetDefaultValues()
        {
            //editTextFrom.SetText(LOCATION, TextView.BufferType.Normal);
            //editTextTo.SetText("IM-N", TextView.BufferType.Normal);
            editTextQty.SetText("0", TextView.BufferType.Normal);
            //editTextDescription.SetText("Stock Transfer TEST", TextView.BufferType.Normal);
        }

        //private async void SetUpSpinners()
        //{
        //    try
        //    {
        //        var locations = await FetchLocationsFromApi();

        //        if (locations != null)
        //        {
        //            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, locations);
        //            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

        //            spinnerFrom.Adapter = adapter;
        //            spinnerTo.Adapter = adapter;

        //            // Set default values for spinners
        //            int locationIndex = locations.IndexOf(LOCATION);
        //            spinnerFrom.SetSelection(locations.IndexOf("RM"));
        //            spinnerTo.SetSelection(locations.IndexOf("IM-N"));
        //        }
        //        else
        //        {
        //            Toast.MakeText(this, "Failed to load locations", ToastLength.Short).Show();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Toast.MakeText(this, "An error occurred: " + ex.Message, ToastLength.Short).Show();
        //    }
        //}



        private async void SetUpSearchableSpinner(AutoCompleteTextView spinner)
        {
            try
            {
                var locations = await FetchLocationsFromApi();
                if (locations != null)
                {
                    var adapter = new SearchableSpinnerAdapter(this, locations);
                    spinner.Adapter = adapter;

                    // Optionally, set default value
                    int locationIndex = locations.IndexOf(LOCATION);
                    spinner.SetText(LOCATION, false);
                }
                else
                {
                    Toast.MakeText(this, "Failed to load locations", ToastLength.Short).Show();
                }
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, "An error occurred: " + ex.Message, ToastLength.Short).Show();
            }
        }

        private async void SetUpProjectSpinners()
        {
            try
            {
                var projects = await FetchProjectsFromApi();

                if (projects != null)
                {
                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, projects);
                    spinnerProject.Adapter = adapter;
                    spinnerProject.SetText(PROJECT, false); // Set default value
                }
                else
                {
                    Toast.MakeText(this, "Failed to load projects", ToastLength.Short).Show();
                }
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, "An error occurred: " + ex.Message, ToastLength.Short).Show();
            }
        }

        private async void SetUpCompanyCodeSpinner()
        {
            try
            {
                var companyCodes = await FetchCompanyCodesFromApi();

                if (companyCodes != null)
                {
                    var adapter = new SearchableCompanyCodeAdapter(this, companyCodes);
                    spinnerCompanyCode.Adapter = adapter;
                    spinnerCompanyCode.Threshold = 1; // Set minimum number of characters to trigger filtering
                }
                else
                {
                    Toast.MakeText(this, "Failed to load company codes", ToastLength.Short).Show();
                }
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, "An error occurred: " + ex.Message, ToastLength.Short).Show();
            }
        }


        //private async void SetUpCompanyCodeSpinner()
        //{
        //    try
        //    {
        //        var companyCodes = await FetchCompanyCodesFromApi();

        //        if (companyCodes != null)
        //        {
        //            var adapter = new CompanyCodeAdapter(this, companyCodes);
        //            spinnerCompanyCode.Adapter = adapter;

        //            // Optionally, set a default selection
        //            spinnerCompanyCode.SetSelection(0);
        //        }
        //        else
        //        {
        //            Toast.MakeText(this, "Failed to load company codes", ToastLength.Short).Show();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Toast.MakeText(this, "An error occurred: " + ex.Message, ToastLength.Short).Show();
        //    }
        //}

        private async Task<List<string>> FetchLocationsFromApi()
        {
            string apiUrl = "http://169.254.176.239:5264/api/RawMaterial/GetLocation";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<string>>(json);
                }
                return null;
            }
        }

        private async Task<List<string>> FetchProjectsFromApi()
        {
            string apiUrl = "http://169.254.176.239:5264/api/RawMaterial/GetProjectData";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<string>>(json);
                }
                return null;
            }
        }

        private async Task<List<CompanyCode>> FetchCompanyCodesFromApi()
        {
            string apiUrl = "http://169.254.176.239:5264/api/RawMaterial/GetCompanyCode";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CompanyCode>>(json);
                }
                return null;
            }
        }

        //private async void ButtonSubmit_Click(object sender, EventArgs e)
        //{
        //    // Validate input
        //    if (string.IsNullOrEmpty(editTextFrom.Text) ||
        //        string.IsNullOrEmpty(editTextTo.Text) ||
        //        string.IsNullOrEmpty(editTextQty.Text) ||
        //        string.IsNullOrEmpty(editTextDescription.Text))
        //    {
        //        Toast.MakeText(this, "Please fill in all fields", ToastLength.Short).Show();
        //        return;
        //    }

        //    // Parse quantity
        //    if (!decimal.TryParse(editTextQty.Text, out decimal qty))
        //    {
        //        Toast.MakeText(this, "Invalid quantity", ToastLength.Short).Show();
        //        return;
        //    }

        //    // Create object with input data
        //    var checkoutData = new CheckoutData
        //    {
        //        Dtlkey = DTLKEY,
        //        From = editTextFrom.Text,
        //        To = editTextTo.Text,
        //        Qty = qty,
        //        Description = editTextDescription.Text
        //    };

        //    bool result = await SendDataToServer(checkoutData);

        //    if (result)
        //    {
        //        Toast.MakeText(this, "Data submitted successfully", ToastLength.Short).Show();
        //        // Optionally, you can navigate back to DisplayDataActivity or another activity
        //        Finish(); // Finish current activity
        //        Intent intent = new Intent(this, typeof(MainActivity));
        //        StartActivity(intent);
        //    }
        //    else
        //    {
        //        //Toast.MakeText(this, "Failed to submit data", ToastLength.Short).Show();
        //        Intent intent = new Intent(this, typeof(MainActivity));
        //        StartActivity(intent);
        //    }
        //}

        private async void ButtonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(editTextQty.Text) || string.IsNullOrEmpty(editTextDescription.Text))
                {
                    Toast.MakeText(this, "Please fill in all fields", ToastLength.Short).Show();
                    return;
                }

                if (!decimal.TryParse(editTextQty.Text, out decimal qty))
                {
                    Toast.MakeText(this, "Invalid quantity", ToastLength.Short).Show();
                    return;
                }

                var from = spinnerFrom.Text;
                var to = spinnerTo.Text;
                var selectedCompanyCode = spinnerCompanyCode.Text;
                var selectedProject = spinnerProject.Text;

                var checkoutData = new CheckoutData
                {
                    Dtlkey = DTLKEY,
                    From = from,
                    To = to,
                    Qty = qty,
                    Description = editTextDescription.Text,
                    Project = selectedProject,
                    CompanyCode = selectedCompanyCode
                };

                ProgressDialog progressDialog = new ProgressDialog(this);
                progressDialog.SetMessage("Submitting data...");
                progressDialog.SetCancelable(false);
                progressDialog.Show();

                bool result = await SendDataToServer(checkoutData);

                progressDialog.Dismiss();

                if (result)
                {
                    ShowAlertDialog("Notice", "Data submitted successfully");
                }
                else
                {
                    ShowAlertDialog("Alert!", "Data Submission Failed!");
                }
            }
            catch (System.Exception ex)
            {
                ShowAlertDialog("Alert!", "An error occurred: " + ex.Message);
            }
        }


        private async Task<bool> SendDataToServer(CheckoutData checkoutData)
        {
            try
            {
                string apiUrl1 = "http://169.254.176.239:5264/api/RawMaterial/AddSTXF";
                using (HttpClient client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(checkoutData);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiUrl1, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string apiUrl2 = $"http://169.254.176.239:5264/api/RawMaterial/Checkout?dtlkey={DTLKEY}&checkoutQty={editTextQty.Text}";
                        StringContent content1 = new StringContent(json, Encoding.UTF8, "application/json");
                        HttpResponseMessage response1 = await client.PutAsync(apiUrl2, null);

                        return response1.IsSuccessStatusCode;
                    }

                    return response.IsSuccessStatusCode;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                ShowAlertDialog("Alert!", $"An Error occur: {ex.Message}");

                return false;
            }
            
        }

        private void ShowAlertDialog(string title, string message)
        {
            Android.Support.V7.App.AlertDialog.Builder dialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            dialogBuilder.SetTitle(title);
            dialogBuilder.SetMessage(message);
            dialogBuilder.SetPositiveButton("OK", (sender, args) =>
            {
                Finish();
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            });
            dialogBuilder.Show();
        }
    }

    public class CheckoutData
    {
        public int Dtlkey { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Qty { get; set; }
        public string Description { get; set; }
        public string Project { get; set; }
        public string CompanyCode { get; set; }
    }

    public class CompanyCode : Java.Lang.Object, Android.Runtime.IJavaObject
    {
        public string Code { get; set; }
        public string CompanyName { get; set; }

        public override string ToString()
        {
            return $"{Code} - {CompanyName}";
        }
    }


    //public class CompanyCodeAdapter : BaseAdapter<CompanyCode>
    //{
    //    private List<CompanyCode> items;
    //    private Context context;

    //    public CompanyCodeAdapter(Context context, List<CompanyCode> items)
    //    {
    //        this.context = context;
    //        this.items = items;
    //    }

    //    public override CompanyCode this[int position] => items[position];

    //    public override int Count => items.Count;

    //    public override long GetItemId(int position) => position;

    //    public override View GetView(int position, View convertView, ViewGroup parent)
    //    {
    //        var view = convertView ?? LayoutInflater.From(context).Inflate(Android.Resource.Layout.SimpleSpinnerItem, parent, false);
    //        var codeTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
    //        codeTextView.Text = $"{items[position].Code} - {items[position].CompanyName}";
    //        return view;
    //    }

    //    public override View GetDropDownView(int position, View convertView, ViewGroup parent)
    //    {
    //        var view = convertView ?? LayoutInflater.From(context).Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem, parent, false);
    //        var codeTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
    //        codeTextView.Text = $"{items[position].Code} - {items[position].CompanyName}";
    //        return view;
    //    }
    //}

    public class SearchableCompanyCodeAdapter : ArrayAdapter<CompanyCode>
    {
        private List<CompanyCode> items;
        private LayoutInflater inflater;

        public SearchableCompanyCodeAdapter(Context context, List<CompanyCode> items) : base(context, Android.Resource.Layout.SimpleDropDownItem1Line, items)
        {
            this.items = items;
            inflater = LayoutInflater.From(context);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? inflater.Inflate(Android.Resource.Layout.SimpleDropDownItem1Line, parent, false);
            var companyCode = items[position];
            var textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            textView.Text = companyCode.ToString();
            return view;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            return GetView(position, convertView, parent);
        }

        public override Filter Filter => new CompanyCodeFilter(this);

        // Ensure items are correctly populated with CompanyCode objects

        public class CompanyCodeFilter : Filter
        {
            private readonly SearchableCompanyCodeAdapter adapter;

            public CompanyCodeFilter(SearchableCompanyCodeAdapter adapter)
            {
                this.adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var filterResults = new FilterResults();

                if (adapter.items == null)
                {
                    return filterResults; // Return empty results if items list is null
                }

                // Filter the original data based on the constraint
                var suggestions = adapter.items
                                        .Where(companyCode => companyCode.ToString().ToLower().Contains(constraint?.ToString().ToLower() ?? ""))
                                        .ToList();

                filterResults.Values = FromArray(suggestions.Select(r => (Java.Lang.Object)r).ToArray());
                filterResults.Count = suggestions.Count;

                return filterResults;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                adapter.Clear();
                if (results != null && results.Values is Java.Lang.Object[] objects)
                {
                    foreach (var obj in objects)
                    {
                        if (obj is JavaObjectWrapper<CompanyCode> wrapper)
                        {
                            var companyCode = wrapper.GetInstance();
                            adapter.Add(companyCode);
                        }
                    }
                }
                adapter.NotifyDataSetChanged();
            }

        }
    }





    public class SearchableSpinnerAdapter : BaseAdapter<string>, IFilterable
    {
        private List<string> originalData;
        private List<string> filteredData;
        private Activity context;
        private Filter filter;

        public SearchableSpinnerAdapter(Activity context, List<string> data)
        {
            this.context = context;
            this.originalData = data;
            this.filteredData = data;
            this.filter = new SearchableSpinnerFilter(this); // Instantiate with correct parameters
        }

        public override int Count => filteredData.Count;

        public override long GetItemId(int position) => position;

        public override string this[int position] => filteredData[position];

        public Filter Filter => filter;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleSpinnerItem, parent, false);
            var textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            textView.Text = filteredData[position];
            return view;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem, parent, false);
            var textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            textView.Text = filteredData[position];
            return view;
        }

        private class SearchableSpinnerFilter : Filter
        {
            private readonly SearchableSpinnerAdapter adapter;

            public SearchableSpinnerFilter(SearchableSpinnerAdapter adapter)
            {
                this.adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var filterResults = new FilterResults();

                if (adapter.originalData == null)
                {
                    return filterResults; // Return empty results if originalData is null
                }

                // Filter the original data based on the constraint
                var suggestions = adapter.originalData
                                        .Where(d => d != null && d.ToLower().Contains(constraint?.ToString().ToLower() ?? ""))
                                        .ToList();

                filterResults.Values = FromArray(suggestions.Select(r => (Java.Lang.Object)new Java.Lang.String(r)).ToArray());
                filterResults.Count = suggestions.Count;

                return filterResults;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                adapter.context.RunOnUiThread(() =>
                {
                    adapter.filteredData.Clear();

                    if (results != null && results.Values != null)
                    {
                        // Explicitly specify the type argument (JavaObjectWrapper<CompanyCode>) for ToArray
                        var enumerable = results.Values.ToArray<JavaObjectWrapper<CompanyCode>>();
                        adapter.filteredData.AddRange(enumerable.Select(r => r.ToString()));
                    }

                    adapter.NotifyDataSetChanged();
                });
            }
        }


    }




}