using Android.App;
using Android.Content;
using Android.Device;
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
using static Android.Util.EventLogTags;
using static Xamarin_Scanner_example.FinishedGoodsActivity;

namespace Xamarin_Scanner_example
{
    [Activity(Label = "Finished Goods")]
    public class FinishedGoodsActivity : AppCompatActivity
    {
        private ScanManager mScanManager;
        //Spinner spinnerCustomer;
        AutoCompleteTextView autoCompleteCustomer;
        TextView dataTextView_Operation, dataTextView_OperationOperation, dataTextView_orderArticle;
        EditText dataEditText_orderUserfield65;
        Button buttonFinishedGoods;
        string CUSTOMER;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_finished_goods);

            mScanManager = new ScanManager();
            mScanManager.StopDecode();

            //dataTextView = FindViewById<TextView>(Resource.Id.dataTextView);
            dataTextView_Operation = FindViewById<TextView>(Resource.Id.dataTextView_Operation);
            dataTextView_OperationOperation = FindViewById<TextView>(Resource.Id.dataTextView_OperationOperation);
            dataTextView_orderArticle = FindViewById<TextView>(Resource.Id.dataTextView_orderArticle);
            dataEditText_orderUserfield65 = FindViewById<EditText>(Resource.Id.dataEditText_orderUserfield65);
            //spinnerCustomer = FindViewById<Spinner>(Resource.Id.spinnerCustomer);
            autoCompleteCustomer = FindViewById<AutoCompleteTextView>(Resource.Id.autoCompleteCustomer);

            buttonFinishedGoods = FindViewById<Button>(Resource.Id.buttonFinishedGoods);
            buttonFinishedGoods.Click += ButtonFinishedGoods_Click;

            string scanResultUrl = Intent.GetStringExtra("ScanResultUrl");

            //SetUpSpinners();
            SetUpCustomerAutoComplete();

            if (!string.IsNullOrEmpty(scanResultUrl))
            {
                FetchAndDisplayData(scanResultUrl);
            }


        }

        private string _OperationId;
        private string _OperationOperation;
        private string _orderArticle;
        private int _orderUserfield65;

        //private async void SetUpSpinners()
        //{
        //    try
        //    {
        //        var customer = await FetchCustomersFromApi();

        //        if (customer != null)
        //        {
        //            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, customer);
        //            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

        //            spinnerCustomer.Adapter = adapter;

        //            // Set default values for spinners
        //            int customerIndex = customer.IndexOf(CUSTOMER);
        //            spinnerCustomer.SetSelection(customer.IndexOf("----"));
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

        private async void SetUpCustomerAutoComplete()
        {
            try
            {
                var projects = await FetchCustomersFromApi();

                if (projects != null)
                {
                    var adapter = new ProjectAutoCompleteAdapter(this, projects);
                    autoCompleteCustomer.Adapter = adapter;

                    autoCompleteCustomer.ItemClick += (s, e) =>
                    {
                        var selectedCustomer = adapter.GetFilteredItem(e.Position);
                        autoCompleteCustomer.Text = selectedCustomer;
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

        private async Task<bool> SendDataToServer(FGTransferData FgTransferData)
        {

            string apiUrl1 = "http://169.254.176.239:5264/api/FG/AddSTAS";
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(FgTransferData);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl1, content);

                return response.IsSuccessStatusCode;
            }
        }

        private async Task<List<string>> FetchCustomersFromApi()
        {
            string apiUrl = "http://169.254.176.239:5264/api/FG/GetCustomerData";
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

        private async void FetchAndDisplayData(string apiUrl)
        {
            ProgressDialog progressDialog = new ProgressDialog(this);
            progressDialog.SetMessage("Fetching data...");
            progressDialog.SetCancelable(false); // Optional, if you don't want the user to cancel it
            progressDialog.Show();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<ApiResponse>(json);
                        DisplayData(data);

                        _OperationId = data.OperationId;
                        _OperationOperation = data.OperationOperation;
                        _orderArticle = data.orderArticle;
                        _orderUserfield65 = data.orderUserfield65;

                    }
                    else
                    {
                        ShowAlertDialog("Alert!", "Unable to Connect To Server");
                    }
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                ShowAlertDialog("Network Error", "A network error occurred: " + httpRequestException.Message);
            }
            catch (TaskCanceledException taskCanceledException)
            {
                ShowAlertDialog("Timeout", "The request timed out: " + taskCanceledException.Message);
            }
            catch (Exception ex)
            {
                ShowAlertDialog("Error", "An unexpected error occurred: " + ex.Message);
            }
            finally
            {
                // Dismiss the loading dialog
                progressDialog.Dismiss();
            }

            
        }


        private async void ButtonFinishedGoods_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(dataEditText_orderUserfield65.Text, out int qty))
            {
                Toast.MakeText(this, "Invalid quantity", ToastLength.Short).Show();
                return;
            }

            var FgTransferData = new FGTransferData 
            {
                OperationId = _OperationId,
                OperationOperation = _OperationOperation,
                orderArticle = _orderArticle,
                orderUserfield65 = qty,
                //Customer = spinnerCustomer.SelectedItem.ToString(),
                Customer = autoCompleteCustomer.Text,
            };

            

            ProgressDialog progressDialog = new ProgressDialog(this);
            progressDialog.SetMessage("Submitting data...");
            progressDialog.SetCancelable(false); // Optional, if you don't want the user to cancel it
            progressDialog.Show();

            try
            {
                bool result = await SendDataToServer(FgTransferData);

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

        private void ShowAlertDialog(string title, string message)
        {
            Android.Support.V7.App.AlertDialog.Builder dialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            dialogBuilder.SetTitle(title);
            dialogBuilder.SetMessage(message);
            dialogBuilder.SetPositiveButton("OK", (sender, args) =>
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            });
            dialogBuilder.Show();
        }

        //private void ShowWarningDialog(string title, string message)
        //{
        //    Android.Support.V7.App.AlertDialog.Builder dialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
        //    dialogBuilder.SetTitle(title);
        //    dialogBuilder.SetMessage(message);
        //    dialogBuilder.SetPositiveButton("Yes", async (sender, args) =>
        //    {
        //        // Perform the intended action
        //        bool state = await CheckoutAndIn();
        //        if (state)
        //        {
        //            Intent intent = new Intent(this, typeof(MainActivity));
        //            StartActivity(intent);
        //        }
        //        else
        //        {
        //            ShowAlertDialog("Alert", "Checkin/Checkout has Failed");
        //        }
        //    });
        //    dialogBuilder.SetNegativeButton("No", (sender, args) =>
        //    {
        //        Intent intent = new Intent(this, typeof(MainActivity));
        //        StartActivity(intent);
        //    });
        //    dialogBuilder.Show();
        //}


        //private async Task<bool> CheckoutAndIn()
        //{
        //    string apiUrl = $"http://169.254.176.239:5111/api/qrcodescan/{Id}/checkin";
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = await client.GetAsync(apiUrl);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string result = await response.Content.ReadAsStringAsync();
        //            return true;
        //        }
        //        else
        //        {
        //            // Handle the error (log it, display a message, etc.)
        //            return false;
        //        }
        //    }
        //}

        private void DisplayData(ApiResponse data)
        {
            dataTextView_Operation.Text = data.OperationId;
            dataTextView_OperationOperation.Text = data.OperationOperation;
            dataTextView_orderArticle.Text = data.orderArticle;
            dataEditText_orderUserfield65.SetText($"{data.orderUserfield65}", TextView.BufferType.Normal);
        }

        public class ApiResponse
        {
            public string OperationId { get; set; } //DOCNO
            public string OperationOperation { get; set; }
            public string orderArticle { get; set; } //DESCRIPTION
            public int orderUserfield65 { get; set; }
        }

        public class FGTransferData
        {
            public string OperationId { get; set; } //DOCNO
            public string OperationOperation { get; set; }
            public string orderArticle { get; set; } //DESCRIPTION
            public int orderUserfield65 { get; set; }
            public string Customer { get; set; }
        }
    }
}