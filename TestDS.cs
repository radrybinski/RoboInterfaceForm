using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Diagnostics;

namespace RoboInterfaceForm
{
    internal class TestDS
    {

        public void Test()
        {

            string API_KEY = "C5GwSTpnxtaa8AftKPAN"; // Your API Key
            string imageURL = "https://i.ibb.co/jzr27x0/YOUR-IMAGE.jpg";
            string MODEL_ENDPOINT = "vibro1/2"; // Set model endpoint

            // Construct the URL
            string uploadURL =
                    "https://detect.roboflow.com/" + MODEL_ENDPOINT
                    + "?api_key=" + API_KEY
                    + "&image=" + HttpUtility.UrlEncode(imageURL);

            // Service Point Config
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Configure Http Request
            WebRequest request = WebRequest.Create(uploadURL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = 0;

            // Get Response
            string responseContent = "";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr99 = new StreamReader(stream))
                    {
                        responseContent = sr99.ReadToEnd();
                    }
                }
            }

            Debug.WriteLine(responseContent);


        }

    }
}
