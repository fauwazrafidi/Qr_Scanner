using Android.App;
using Android.Content;
using Android.Device;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using com.companyname.xamarin_scanner_example;

namespace Xamarin_Scanner_example
{
    [Activity(Label = "Raw Material Data")]
    public class DisplayDataActivity : AppCompatActivity
    {
        private TextView dataTextView_Itemcode, dataTextView_Description,
            dataTextView_Description2, dataTextView_Location, dataTextView_Checkin,
            dataTextView_Qtyremain, dataTextView_Grv;
        private ScanManager mScanManager;
        Button buttonCheckout, buttonReturn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.activity_display_data);

                mScanManager = new ScanManager();
                mScanManager.StopDecode();

                //dataTextView = FindViewById<TextView>(Resource.Id.dataTextView);
                dataTextView_Itemcode = FindViewById<TextView>(Resource.Id.dataTextView_Itemcode);
                dataTextView_Description = FindViewById<TextView>(Resource.Id.dataTextView_Description);
                dataTextView_Description2 = FindViewById<TextView>(Resource.Id.dataTextView_Description2);
                dataTextView_Location = FindViewById<TextView>(Resource.Id.dataTextView_Location);
                dataTextView_Qtyremain = FindViewById<TextView>(Resource.Id.dataTextView_Qtyremain);
                dataTextView_Checkin = FindViewById<TextView>(Resource.Id.dataTextView_Checkin);
                dataTextView_Grv = FindViewById<TextView>(Resource.Id.dataTextView_Grv);

                buttonCheckout = FindViewById<Button>(Resource.Id.buttonCheckout);
                buttonCheckout.Click += ButtonCheckout_Click;
                buttonReturn = FindViewById<Button>(Resource.Id.buttonReturn);
                buttonReturn.Click += ButtonReturn_Click;

                string scanResultUrl = Intent.GetStringExtra("ScanResultUrl");
                if (!string.IsNullOrEmpty(scanResultUrl))
                {
                    FetchAndDisplayData(scanResultUrl);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }

        private int DTLKEY;
        private string ITEMCODE;
        private string DESCRIPTION;
        private string LOCATION;
        private decimal QTY;
        private decimal QTYREMAIN;

        //private async void FetchAndDisplayData(string apiUrl)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = await client.GetAsync(apiUrl);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string json = await response.Content.ReadAsStringAsync();
        //            var data = JsonConvert.DeserializeObject<ApiResponse>(json);
        //            DisplayData(data);

        //            DTLKEY = data.Dtlkey;
        //            ITEMCODE = data.Itemcode;
        //            DESCRIPTION = data.Description;
        //            LOCATION = data.Location;
        //            QTY = data.Qty;
        //            QTYREMAIN = data.Qtyremain;


        //        }
        //        else
        //        {
        //            ShowAlertDialog("Alert!", "Unable to Connect To Server");
        //        }
        //    }
        //}

        //private async void FetchAndDisplayData(string apiUrl)
        //{
        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            HttpResponseMessage response = await client.GetAsync(apiUrl);
        //            if (response.IsSuccessStatusCode)
        //            {
        //                string json = await response.Content.ReadAsStringAsync();
        //                var data = JsonConvert.DeserializeObject<ApiResponse>(json);
        //                DisplayData(data);

        //                DTLKEY = data.Dtlkey;
        //                ITEMCODE = data.Itemcode;
        //                DESCRIPTION = data.Description;
        //                LOCATION = data.Location;
        //                QTY = data.Qty;
        //                QTYREMAIN = data.Qtyremain;
        //            }
        //            else
        //            {
        //                ShowAlertDialog("Alert!", "Unable to Connect To Server");
        //            }
        //        }
        //    }
        //    catch (HttpRequestException httpRequestException)
        //    {
        //        ShowAlertDialog("Network Error", "A network error occurred: " + httpRequestException.Message);
        //    }
        //    catch (TaskCanceledException taskCanceledException)
        //    {
        //        ShowAlertDialog("Timeout", "The request timed out: " + taskCanceledException.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowAlertDialog("Error", "An unexpected error occurred: " + ex.Message);
        //    }
        //}

        private async void FetchAndDisplayData(string apiUrl)
        {
            // Create and show the loading dialog
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

                        DTLKEY = data.Dtlkey;
                        ITEMCODE = data.Itemcode;
                        DESCRIPTION = data.Description;
                        LOCATION = data.Location;
                        QTY = data.Qty;
                        QTYREMAIN = data.Qtyremain;
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



        private void DisplayData(ApiResponse data)
        {
            dataTextView_Itemcode.Text = data.Itemcode;
            dataTextView_Description.Text = data.Description;
            dataTextView_Description2.Text = data.Description2;
            dataTextView_Location.Text = data.Location;
            dataTextView_Qtyremain.Text = $"{data.Qtyremain} / {data.Qty} {data.Uom}";
            dataTextView_Checkin.Text = $"{data.Docdate.ToString("dd-MM-yyyy")}";
            dataTextView_Grv.Text = data.Docno;
        }

        private async void ButtonCheckout_Click(object sender, EventArgs e)
        {
            bool canCheckout = await CanCheckout(DTLKEY);
            if (canCheckout)
            {
                Intent checkoutIntent = new Intent(this, typeof(CheckoutActivity));
                checkoutIntent.PutExtra("Dtlkey", DTLKEY); // Pass Dtlkey value
                checkoutIntent.PutExtra("Itemcode", ITEMCODE);
                checkoutIntent.PutExtra("Description", DESCRIPTION);
                checkoutIntent.PutExtra("location", LOCATION);
                StartActivity(checkoutIntent);
            }
            else
            {
                ShowAlertDialog("Notice!", "Cannot proceed with checkout. Not earliest item");
                //Toast.MakeText(this, "Cannot proceed with checkout. Not earliest item", ToastLength.Short).Show();
            }
        }

        private void ButtonReturn_Click(object sender, EventArgs e)
        {
            if (QTY > QTYREMAIN)
            {
                Intent returnIntent = new Intent(this, typeof(ReturnActivity));
                returnIntent.PutExtra("Dtlkey", DTLKEY); // Pass Dtlkey value
                returnIntent.PutExtra("Itemcode", ITEMCODE);
                returnIntent.PutExtra("Description", DESCRIPTION);
                returnIntent.PutExtra("location", LOCATION);
                StartActivity(returnIntent);
            }
            else
            {
                ShowAlertDialog("Notice!", "Quantity is still full");
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

        private async Task<bool> CanCheckout(int dtlkey)
        {
            string apiUrl = $"http://169.254.176.239​:5264/api/RawMaterial/CanCheckout?dtlkey={dtlkey}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return bool.Parse(result); // Assuming the API returns "true" or "false" as a string
                }
                else
                {
                    // Handle the error (log it, display a message, etc.)
                    return false;
                }
            }
        }

        public class ApiResponse
        {
            public int Dockey { get; set; }
            public int Dtlkey { get; set; }
            public string Remark2 { get; set; }
            public int Seq { get; set; }
            public string Itemcode { get; set; }
            public string Description { get; set; }
            public string Description2 { get; set; }
            public string Batch { get; set; }
            public string Project { get; set; }
            public string Location { get; set; }
            public int Receiveqty { get; set; }
            public int Returnqty { get; set; }
            public int Qty { get; set; }
            public string Uom { get; set; }
            public int Qtyremain { get; set; }
            public DateTime Docdate { get; set; }
            public string Docno { get; set; }
        }
    }
}