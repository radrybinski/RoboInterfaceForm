using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.Json.Serialization;

namespace RoboInterfaceForm
{
    public class Detection
    {
        private string csvFile = "";
        private static string pngFile = "";

        private static string modelName = "vibro1";
        private static string modelVersion = "2";
        private static string apiKey = "C5GwSTpnxtaa8AftKPAN";
        private static string apiUrl = "https://detect.roboflow.com/";
        private static string fileName = Path.GetFileName(pngFile);
        private static string uploadUrl = apiUrl + modelName + "/" + modelVersion + "?api_key=" + apiKey + "&name=" + fileName;

        

        public Detection(string filePath, string mName, string mVersion, string key)

        {
            pngFile = filePath;
            modelName = mName;
            modelVersion = mVersion;
            apiKey = key;

            // csvFile = filePath;


        }
        public Detection()
        {


        }

        //public async Task RunDetection()
        public static async Task<InferenceResponse> RunDetection()
        {
            // Load Image and Convert to Base64
            string imagePath = pngFile; // Path to image to upload, e.g., image.jpg
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
            string fileContent = Convert.ToBase64String(imageData);
            //byte[] dataEncoded = Encoding.ASCII.GetBytes(fileContent);
            var postData = new StringContent(fileContent, Encoding.ASCII, "application/x-www-form-urlencoded");

            // Initialize Inference Server Request with API_KEY, Model, and Model Version
            var request = new HttpRequestMessage(HttpMethod.Post, uploadUrl);
            request.Content = postData;



            // Execute Post Request
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode(); // Throws if not a success code

                    // Parse Response to String
                    string responseData = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine(responseData);

                    // Convert Response String to Dictionary
                    var result = JsonSerializer.Deserialize<InferenceResponse>(responseData);
                    

                    // Print String Response
                    //Debug.WriteLine("File: " + Path.GetFileName(pngFile));
                    //Debug.WriteLine($"Top Prediction: {result.Top} with confidence {result.Confidence}");
                    //Debug.WriteLine($"Image Size: {result.Image.Width}x{result.Image.Height}");
                    foreach (var pred in result.Predictions)
                    {
                        //Debug.WriteLine($"Class: {pred.Class}, Confidence: {pred.Confidence}");
                    }

                    Debug.WriteLine("Done...");

                    return result;
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine($"Request error: {e.Message}");

                    //must be something to throw as return
                    return new InferenceResponse
                    {
                        InferenceId = "Error: " + e.Message,
                        Time = 0,
                        Image = new ImageInfo { Width = 0, Height = 0 },
                        Predictions = new List<Prediction>(),
                        Top = "Error",
                        Confidence = 0
                    };

                }
                catch (JsonException e)
                {
                    Debug.WriteLine($"JSON parsing error: {e.Message}");
                    return new InferenceResponse
                    {
                        InferenceId = "JSON parsing error: " + e.Message,
                        Time = 0,
                        Image = new ImageInfo { Width = 0, Height = 0 },
                        Predictions = new List<Prediction>(),
                        Top = "Error",
                        Confidence = 0
                    };
                }
            }
        }


        //Classes to manage JSON response from server


    }
    public class ImageInfo
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }
    }

    public class Prediction
    {
        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("class_id")]
        public int ClassId { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }
    }

    public class InferenceResponse
    {
        [JsonPropertyName("inference_id")]
        public string InferenceId { get; set; }

        [JsonPropertyName("time")]
        public double Time { get; set; }

        [JsonPropertyName("image")]
        public ImageInfo Image { get; set; }

        [JsonPropertyName("predictions")]
        public List<Prediction> Predictions { get; set; }

        [JsonPropertyName("top")]
        public string Top { get; set; }

        [JsonPropertyName("confidence")]
        public double Confidence { get; set; }
    }

}

