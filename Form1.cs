using System.Diagnostics;
using static RoboInterfaceForm.Detection;
namespace RoboInterfaceForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private string filePath = "";
        private string modelName = "";
        private string modelVersion = "";
        private string apiKey = "";


        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select File";
            ofd.InitialDirectory = @"C:\";
            //ofd.Filter = "CSV Files(*.CSV)|*.CSV";
            ofd.Filter = "PNG files(*.PNG;*.JPG)|*.PNG;*.JPG";
            ofd.Multiselect = false;
            ofd.FilterIndex = 1;
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                textBox1.Text = ofd.FileName;
                filePath = ofd.FileName;

            }
            else
            {
                textBox1.Text = "";
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {

            modelName = textBox2.Text;
            modelVersion = textBox3.Text;
            apiKey = textBox4.Text;



            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please enter a valid image path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                LoadImage(filePath);

                Detection DT = new Detection(filePath, modelName, modelVersion, apiKey);
                // Await the async method
                InferenceResponse result = await Detection.RunDetection();

                DisplayResults(result, filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Processing Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void DisplayResults(InferenceResponse result, string file)
        {
            //lblInferenceId.Text = $"Inference ID: {result.InferenceId}";
            //lblTime.Text = $"Time: {result.Time}s";
            //lblImageSize.Text = $"Image Size: {result.Image.Width}x{result.Image.Height}";
            //lblTopPrediction.Text = $"Top Prediction: {result.Top} ({result.Confidence:P2})";

            string fileName = Path.GetFileName(file);


            listBox1.Items.Clear();

            listBox1.Items.Add($"File: {fileName}");
            listBox1.Items.Add($"Inference ID: {result.InferenceId}");
            listBox1.Items.Add($"Time: {result.Time:F3}s");
            listBox1.Items.Add($"Image Size: {result.Image.Width}x{result.Image.Height}");
            listBox1.Items.Add($"Top Prediction: {result.Top} ({result.Confidence:P2})");
            listBox1.Items.Add("--------------------------------");
            foreach (var pred in result.Predictions)
            {
                listBox1.Items.Add($"Class: {pred.Class}, Confidence: {pred.Confidence:P2}");
            }
        }

        private void LoadImage(string imagePath) // load image to Form1
        {
            try
            {
                // Check if the file exists before trying to load it
                if (File.Exists(imagePath))
                {
                    // Load the image into the PictureBox
                    pictureBox1.Image = new System.Drawing.Bitmap(imagePath);
                }
                else
                {
                    MessageBox.Show("Image not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Handle any error in loading the image
                MessageBox.Show($"Failed to load image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PlotCSV pyt = new PlotCSV();

            pyt.GeneratePlot();


        }
    }

}
