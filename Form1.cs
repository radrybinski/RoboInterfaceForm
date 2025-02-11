using System.Diagnostics;
namespace RoboInterfaceForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private string filePath = "";


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

        private void button1_Click(object sender, EventArgs e)
        {


            //DetectionClass ds = new DetectionClass(filePath);


            //ds.RunDetectionAsync();


            TestDS ds = new TestDS();
            ds.Test();


        }
    }

}
