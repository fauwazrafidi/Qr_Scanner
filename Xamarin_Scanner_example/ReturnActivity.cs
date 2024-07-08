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
    [Activity(Label = "Return")]
    public class ReturnActivity : AppCompatActivity
    {
        Spinner spinnerFrom, spinnerTo, spinnerCompanyCode;
        AutoCompleteTextView autoCompleteProject, autoCompleteCompanyCode;
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
            autoCompleteProject = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteProject);
            //spinnerCompanyCode = FindViewById<Spinner>(Resource.Id.spinnerCompanyCode);
            autoCompleteCompanyCode = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteCompanyCode);
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
            SetSpinnerValues();
            //SetUpProjectSpinners();
            SetUpProjectAutoComplete();
            //SetUpCompanyCodeSpinner();
            SetUpCompanyCodeAutoComplete();

            buttonSubmit.Click += ButtonSubmit_Click;
        }

        private void SetDefaultValues()
        {
            editTextQty.SetText("0", TextView.BufferType.Normal);
            //editTextDescription.SetText("Stock Transfer TEST", TextView.BufferType.Normal);
        }

        private async void SetSpinnerValues()
        {
            try
            {
                var locations = await FetchLocationsFromApi();

                if (locations != null)
                {
                    var fromAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, locations);
                    fromAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    spinnerFrom.Adapter = fromAdapter;
                    spinnerFrom.SetSelection(locations.IndexOf("IM-N"));

                    var toAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, locations);
                    toAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                    spinnerTo.Adapter = toAdapter;
                    spinnerTo.SetSelection(locations.IndexOf("RM"));
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

        private async void SetUpProjectAutoComplete()
        {
            try
            {
                var projects = await FetchProjectsFromApi();

                if (projects != null)
                {
                    var adapter = new ProjectAutoCompleteAdapter(this, projects);
                    autoCompleteProject.Adapter = adapter;

                    autoCompleteProject.ItemClick += (s, e) =>
                    {
                        var selectedProject = adapter.GetFilteredItem(e.Position);
                        autoCompleteProject.Text = selectedProject;
                        PROJECT = selectedProject;
                    };
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

        private async void SetUpCompanyCodeAutoComplete()
        {
            try
            {
                var companyCodes = await FetchCompanyCodesFromApi();

                if (companyCodes != null)
                {
                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, companyCodes);
                    autoCompleteCompanyCode.Adapter = adapter;
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

        //private async void SetUpProjectSpinners()
        //{
        //    try
        //    {
        //        var Project = await FetchProjectsFromApi();

        //        if (Project != null)
        //        {
        //            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, Project);
        //            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

        //            spinnerProject.Adapter = adapter;

        //            // Set default values for spinners
        //            int locationIndex = Project.IndexOf(PROJECT);
        //            spinnerProject.SetSelection(Project.IndexOf("----"));
        //        }
        //        else
        //        {
        //            Toast.MakeText(this, "Failed to load Project", ToastLength.Short).Show();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Toast.MakeText(this, "An error occurred: " + ex.Message, ToastLength.Short).Show();
        //    }
        //}

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

        private async Task<List<string>> FetchCompanyCodesFromApi()
        {
            string apiUrl = "http://169.254.176.239:5264/api/RawMaterial/GetCompanyCode";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var companyCodes = JsonConvert.DeserializeObject<List<CompanyCode>>(json);

                    return companyCodes.Select(cc => $"{cc.Code} - {cc.CompanyName}").ToList();
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
        //    var returnData = new ReturnData
        //    {
        //        Dtlkey = DTLKEY,
        //        From = editTextFrom.Text,
        //        To = editTextTo.Text,
        //        Qty = qty,
        //        Description = editTextDescription.Text
        //    };

        //    bool result = await SendDataToServer(returnData);

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
        //        Toast.MakeText(this, "Failed to submit data", ToastLength.Short).Show();
        //        Intent intent = new Intent(this, typeof(MainActivity));
        //        StartActivity(intent);
        //    }
        //}

        private async void ButtonSubmit_Click(object sender, EventArgs e)
        {
            // Validate input
            if (spinnerFrom.SelectedItem == null ||
                spinnerTo.SelectedItem == null ||
                string.IsNullOrEmpty(editTextQty.Text) ||
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
            //var selectedCompanyCode = spinnerCompanyCode.SelectedItem.ToString();
            var selectedCompanyCode = autoCompleteCompanyCode.Text;


            string companyCode = selectedCompanyCode.Substring(0, System.Math.Min(9, selectedCompanyCode.Length));

            // Create object with input data
            var returnData = new ReturnData
            {
                Dtlkey = DTLKEY,
                From = from,
                To = to,
                Qty = qty,
                Description = editTextDescription.Text,
                Project = autoCompleteProject.Text,
                CompanyCode = companyCode,
            };

            // Create and show the loading dialog
            ProgressDialog progressDialog = new ProgressDialog(this);
            progressDialog.SetMessage("Submitting data...");
            progressDialog.SetCancelable(false); // Optional, if you don't want the user to cancel it
            progressDialog.Show();

            try
            {
                bool result = await SendDataToServer(returnData);

                if (result)
                {
                    //Toast.MakeText(this, "Data submitted successfully", ToastLength.Short).Show();
                    //// Optionally, you can navigate back to DisplayDataActivity or another activity
                    //Finish(); // Finish current activity
                    //Intent intent = new Intent(this, typeof(MainActivity));
                    //StartActivity(intent);
                    ShowAlertDialog("Notice", "Data submitted successfully");
                }
                else
                {
                    //Toast.MakeText(this, "Failed to submit data", ToastLength.Short).Show();
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


        private async Task<bool> SendDataToServer(ReturnData returnData)
        {

            string apiUrl1 = "http://169.254.176.239:5264/api/RawMaterial/AddSTXF";
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(returnData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl1, content);

                if (response.IsSuccessStatusCode)
                {
                    string apiUrl2 = $"http://169.254.176.239:5264/api/RawMaterial/Return?dtlkey={DTLKEY}&returnQty={editTextQty.Text}";
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

    public class ReturnData
    {
        public int Dtlkey { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Qty { get; set; }
        public string Description { get; set; }
        public string Project { get; set; }
        public string CompanyCode { get; set; }
    }

}