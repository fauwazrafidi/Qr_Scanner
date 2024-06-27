using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using com.companyname.xamarin_scanner_example;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin_Scanner_example
{
    [Activity(Label = "Checkout")]
    public class CheckoutActivity : AppCompatActivity
    {
        Spinner spinnerFrom, spinnerTo, spinnerProject, spinnerCompanyCode;
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

            spinnerFrom = FindViewById<Spinner>(Resource.Id.spinnerFrom);
            spinnerTo = FindViewById<Spinner>(Resource.Id.spinnerTo);
            spinnerProject = FindViewById<Spinner>(Resource.Id.spinnerProject);
            spinnerCompanyCode = FindViewById<Spinner>(Resource.Id.spinnerCompanyCode);
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
            SetUpSpinners();
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

        private async void SetUpSpinners()
        {
            try
            {
                var locations = await FetchLocationsFromApi();

                if (locations != null)
                {
                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, locations);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                    spinnerFrom.Adapter = adapter;
                    spinnerTo.Adapter = adapter;

                    // Set default values for spinners
                    int locationIndex = locations.IndexOf(LOCATION);
                    spinnerFrom.SetSelection(locations.IndexOf("RM"));
                    spinnerTo.SetSelection(locations.IndexOf("IM-N"));
                }
                else
                {
                    Toast.MakeText(this, "Failed to load locations", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "An error occurred: " + ex.Message, ToastLength.Short).Show();
            }
        }

        private async void SetUpProjectSpinners()
        {
            try
            {
                var Project = await FetchProjectsFromApi();

                if (Project != null)
                {
                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Project);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                    spinnerProject.Adapter = adapter;

                    // Set default values for spinners
                    int locationIndex = Project.IndexOf(PROJECT);
                    spinnerProject.SetSelection(Project.IndexOf("----"));
                }
                else
                {
                    Toast.MakeText(this, "Failed to load Project", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
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
                    var adapter = new CompanyCodeAdapter(this, companyCodes);
                    spinnerCompanyCode.Adapter = adapter;

                    // Optionally, set a default selection
                    spinnerCompanyCode.SetSelection(0);
                }
                else
                {
                    Toast.MakeText(this, "Failed to load company codes", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "An error occurred: " + ex.Message, ToastLength.Short).Show();
            }
        }

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
            // Validate input
            if (string.IsNullOrEmpty(editTextQty.Text) ||
                string.IsNullOrEmpty(editTextDescription.Text))
            {
                Toast.MakeText(this, "Please fill in all fields", ToastLength.Short).Show();
                return;
            }

            // Parse quantity
            if (!decimal.TryParse(editTextQty.Text, out decimal qty))
            {
                Toast.MakeText(this, "Invalid quantity", ToastLength.Short).Show();
                return;
            }

            var from = spinnerFrom.SelectedItem.ToString();
            var to = spinnerTo.SelectedItem.ToString();
            var selectedCompanyCode = spinnerCompanyCode.SelectedItem as CompanyCode;

            // Create object with input data
            var checkoutData = new CheckoutData
            {
                Dtlkey = DTLKEY,
                From = from,
                To = to,
                Qty = qty,
                Description = editTextDescription.Text,
                Project = spinnerProject.SelectedItem.ToString(),
                CompanyCode = selectedCompanyCode?.Code,
            };

            // Create and show the loading dialog
            ProgressDialog progressDialog = new ProgressDialog(this);
            progressDialog.SetMessage("Submitting data...");
            progressDialog.SetCancelable(false); // Optional, if you don't want the user to cancel it
            progressDialog.Show();

            try
            {
                bool result = await SendDataToServer(checkoutData);

                if (result)
                {
                    ShowAlertDialog("Notice", "Data submitted successfully");
                }
                else
                {
                    //// Optionally, handle failure
                    //Intent intent = new Intent(this, typeof(MainActivity));
                    //StartActivity(intent);
                    ShowAlertDialog("Alert!", "Data Submission Failed!");
                }
            }
            catch (Exception ex)
            {
                //Toast.MakeText(this, "An error occurred: " + ex.Message, ToastLength.Short).Show();
                ShowAlertDialog("Alert!", "An error occurred: " + ex.Message);
            }
            finally
            {
                // Dismiss the loading dialog
                progressDialog.Dismiss();
            }
        }


        private async Task<bool> SendDataToServer(CheckoutData checkoutData)
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
    }

    public class CompanyCodeAdapter : BaseAdapter<CompanyCode>
    {
        private List<CompanyCode> items;
        private Context context;

        public CompanyCodeAdapter(Context context, List<CompanyCode> items)
        {
            this.context = context;
            this.items = items;
        }

        public override CompanyCode this[int position] => items[position];

        public override int Count => items.Count;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(context).Inflate(Android.Resource.Layout.SimpleSpinnerItem, parent, false);
            var codeTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            codeTextView.Text = $"{items[position].Code} - {items[position].CompanyName}";
            return view;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(context).Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem, parent, false);
            var codeTextView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            codeTextView.Text = $"{items[position].Code} - {items[position].CompanyName}";
            return view;
        }
    }
}