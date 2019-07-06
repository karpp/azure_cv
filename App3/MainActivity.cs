using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V4.App;
using Android;
using Android.Content;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace App3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button btn;
        private TextView text;

        public const string key = "<key>";
        public readonly HttpClient client = new HttpClient
        {
            DefaultRequestHeaders = { { "Ocp-Apim-Subscription-Key", key } }
        };
        public string baseurl = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/analyze";
        //public string baseurl = "https://en401t57mzxey.x.pipedream.net";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            SetContentView(Resource.Layout.activity_main);
            btn = (Button)FindViewById(Resource.Id.btn);
            text = (TextView)FindViewById(Resource.Id.text);
            btn.Click += delegate {
                Intent image = new Intent();
                image.SetType("image/*");
                image.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(image, "choose"), 57);
                //text.Text = login.Text;
            };


            ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, 57);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                string path = ActualPath.GetActualPathFromFile(data.Data, this);
                text.Text = path;
                Analyze(path);
            }
        }

        async Task Analyze(string path)
        {
            string param = "visualFeatures=Description";
            string url = baseurl + "?" + param;
            HttpResponseMessage response;
            byte[] img = GetBytesFromImage(path);
            text.Text = "analyze1";
            using (ByteArrayContent content = new ByteArrayContent(img))
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                text.Text = "analyze2";
                response = await client.PostAsync(url, content);
                text.Text = "analyze3";
            }
            text.Text = "analyze4";
            string resp = await response.Content.ReadAsStringAsync();
            text.Text = "analyze5";
            text.Text = JToken.Parse(resp).ToString();
            //JToken data = JToken.Parse(resp);
            //string tmp = data["description"]["captions"][0]["text"].ToString();
            //text.Text = tmp;
        }

        private byte[] GetBytesFromImage(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(stream);
                return reader.ReadBytes((int)stream.Length);
            }
        }
    }
}
