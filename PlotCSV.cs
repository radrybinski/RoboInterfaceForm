using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Runtime; //Python package
using System.Diagnostics;
using Microsoft.VisualBasic.ApplicationServices;

namespace RoboInterfaceForm
{
    class PlotCSV
    {
        string folderName = "STFT";
        string csvFile = @"C:\Users\r.rybinski\Downloads\csv-to-png\003\20250204-003_079.csv";

        public void GeneratePlot()
        {
            //PythonEngine.PythonHome = @"C:\Users\r.rybinski\AppData\Local\Programs\Python\Python311";

            //PythonEngine.PythonHome = @"C:\Users\r.rybinski\AppData\Local\Programs\Python\Python311";

            Runtime.PythonDLL = @"C:\Users\r.rybinski\AppData\Local\Programs\Python\Python311\python311.dll";

            // Initialize Python Engine
            PythonEngine.Initialize();

            using (Py.GIL()) // Ensure thread safety
            {
                dynamic sys = Py.Import("sys");
                string ver = sys.version;
                Debug.WriteLine(ver);
                sys.path.append(@"C:\Users\r.rybinski\source\repos\RoboInterfaceForm");


                dynamic pyScript = Py.Import("pyt");  // Import Python script (myscript.py)
                dynamic result = pyScript.run_stft(folderName, csvFile); // Call Python function

                Debug.WriteLine("Done...!");
                //dynamic sys = Py.Import("sys");
                //Debug.WriteLine($"Python version: {sys.version}");

                //var result = pyScript.InvokeMethod("hello");
                //Debug.WriteLine(string res = result.ToString());



            }

            PythonEngine.Shutdown(); // Properly shutdown Python Engine
        }
    }


}

