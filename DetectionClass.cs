using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using System.Drawing;

namespace RoboInterfaceForm
{
    internal class DetectionClass
    {

        private string csvFile = "";
        private string pngFile = "";

        private static string apiKey = "C5GwSTpnxtaa8AftKPAN";
        private static string apiUrl = "https://classify.roboflow.com/vibro1/2?api_key=" + apiKey;

        public DetectionClass(string filePath) 
        
        {
            pngFile = filePath;
            // csvFile = filePath;

            


        }


            

        
        public async Task RunDetectionAsync()
        {

            Debug.WriteLine(pngFile);
            try
            {
                string base64Image = ConvertImageToBase64(pngFile);
                

                string response = await PostImageToRoboflow(apiUrl, base64Image);
                Console.WriteLine("Response from Roboflow:");
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }



        }

        static string ConvertImageToBase64(string imagePath)
        {
            using (Image image = Image.FromFile(imagePath))
            {
                // Resize the image if necessary
                Image resizedImage = ResizeImage(image, 1500, 1500);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    resizedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] imageBytes = memoryStream.ToArray();
                    return "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
                }
            }
        }
        /*
        static async Task<string> PostImageToRoboflow(string url, string base64Image)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(base64Image, Encoding.ASCII, "application/octet-stream");
                //HttpResponseMessage response = await client.PostAsync(url, content);
                var response = await client.PostAsync(url, content);
                //response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                //return await response.Content.ReadAsStringAsync();
                return responseString;
            }
        }
        */
        static Image ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            int width = image.Width;
            int height = image.Height;

            if (width > height)
            {
                if (width > maxWidth)
                {
                    height = (int)(height * (maxWidth / (float)width));
                    width = maxWidth;
                }
            }
            else
            {
                if (height > maxHeight)
                {
                    width = (int)(width * (maxHeight / (float)height));
                    height = maxHeight;
                }
            }

            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        static async Task<string> PostImageToRoboflow(string url, string base64Image)
        {
            using (HttpClient client = new HttpClient())
            {
                // Prepare JSON payload
                string jsonPayload = $"{{\"image\":\"{base64Image}\"}}";

                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = await client.PostAsync(url, content);
                var response = await client.PostAsync(url, content);
                //response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }

    }
}
