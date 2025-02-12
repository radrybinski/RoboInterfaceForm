using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Runtime; //Python package

namespace RoboInterfaceForm
{
     class PlotCSV
    {
        static void GeneratePlot()
        {
            // Initialize Python Engine
            PythonEngine.Initialize();

            using (Py.GIL()) // Ensure thread safety
            {
                dynamic pyScript = Py.Import("myscript");  // Import Python script (myscript.py)
                dynamic result = pyScript.some_function(); // Call Python function
                Console.WriteLine(result);
            }

            PythonEngine.Shutdown(); // Properly shutdown Python Engine
        }
    }


}
}
