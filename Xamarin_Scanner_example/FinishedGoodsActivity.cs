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

namespace Xamarin_Scanner_example
{
    [Activity(Label = "Finished Goods")]
    public class FinishedGoodsActivity : AppCompatActivity
    {
        private ScanManager mScanManager;
        Spinner spinnerCustomer;
        TextView dataTextView_Operation, dataTextView_OperationOperation, dataTextView_orderArticle, dataTextView_orderUserfield65;
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
            dataTextView_orderUserfield65 = FindViewById<TextView>(Resource.Id.dataTextView_orderUserfield65);
            spinnerCustomer = FindViewById<Spinner>(Resource.Id.spinnerCustomer);

            buttonFinishedGoods = FindViewById<Button>(Resource.Id.buttonFinishedGoods);
            buttonFinishedGoods.Click += ButtonFinishedGoods_Click;

            string scanResultUrl = Intent.GetStringExtra("ScanResultUrl");

            SetUpSpinners();

            if (!string.IsNullOrEmpty(scanResultUrl))
            {
                FetchAndDisplayData(scanResultUrl);
            }


        }

        private string OperationId;
        private string OperationOperation;
        private string orderArticle;
        private int orderUserfield65;

        private async void SetUpSpinners()
        {
            try
            {
                var customer = await FetchCustomersFromApi();

                if (customer != null)
                {
                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, customer);
                    adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                    spinnerCustomer.Adapter = adapter;

                    // Set default values for spinners
                    int customerIndex = customer.IndexOf(CUSTOMER);
                    spinnerCustomer.SetSelection(customer.IndexOf("C-AA Products"));
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

        private async Task<bool> SendDataToServer(CheckoutData checkoutData)
        {

            string apiUrl1 = "http://169.254.176.239:5264/api/RawMaterial/AddSTAS";
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(checkoutData);
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
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ApiResponse>(json);
                    DisplayData(data);

                    OperationId = data.OperationId;
                    OperationOperation = data.OperationOperation;
                    orderArticle = data.orderArticle;
                    orderUserfield65 = data.orderUserfield65;

                }
                else
                {
                    ShowAlertDialog("Alert!", "Unable to Connect To Server");
                }
            }
        }

        //private async void ButtonFinishedGoods_Click(object sender, EventArgs e)
        //{
        //    if (CheckOut)
        //    {
        //        ShowAlertDialog("Alert!", "Finished Goods has Checked Out");
        //    }
        //    else if(CheckIn)
        //    {
        //        ShowWarningDialog("Notice", "This is For CheckOut. Do you approve?");
        //    }
        //    else
        //    {
        //        ShowWarningDialog("Notice", "This is For CheckIn. Do you approve?");
        //    }
        //}

        private async void ButtonFinishedGoods_Click(object sender, EventArgs e)
        {

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
            dataTextView_orderUserfield65.Text = $"{data.orderUserfield65}";
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