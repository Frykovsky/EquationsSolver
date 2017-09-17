using System;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Domain.UsersDB;
using System.Text;

namespace Domain.EquationsRelated
{
    public static class EquationInitializeConditions
    {
        public static double[] Triangle(int Len)
        {
            double[] data = new double[Len];
            for (int j = 0; j < Len; j++)
            {
                if (j == Len / 2) data[j] = 1.0;
                else data[j] = 0.0;
            }
            return data;
        }

        public static double[] Gauss(int Len)
        {
            double[] data = new double[Len];
            for (int j = 0; j < Len; j++)
            {
                data[j] = Math.Exp(-Math.Pow(j - 0.5*Len, 2));
            }
            return data;
        }

        public static double[] Rectangle(int Len)
        {
            double[] data = new double[Len];
            for (int j = 0; j < Len; j++)
            {
                if (j > Len / 3 && j < Len * 2 / 3) data[j] = 1.0;
                else data[j] = 0.0;
            }
            return data;
        }
    }

    public static class EquationMethods
    { 
        public static string FileWriter(double [] data, EquationParameters parames, int time)
        {
            //StreamWriter wrt = new StreamWriter(stream);
            //wrt.WriteLine(time==0 ? "[":"");
            StringBuilder wrt = new StringBuilder();
            wrt.AppendLine(time == 0 ? "[" : "");
            for (int j = 0; j < data.Length; j++)
            {
                double val = j * parames.VarStep;
                wrt.AppendLine("{\"x\":" + val.ToString("0.00") + ",");
                wrt.AppendLine("\"t\":" + (time).ToString() + ",");
                wrt.AppendLine("\"val\":" + data[j].ToString("0.00") + (time == (int)(parames.MaxTime / parames.TimeStep)-1 && j==(data.Length-1) ? "}]" : "}," ));
            }
            return wrt.ToString();
        }

        public static double [] InitialConditionsSetter (EquationParameters parames, int dataLength)
        {
            Type MyType = typeof(EquationInitializeConditions);
            MethodInfo[] methods = MyType.GetMethods();
            double[] data = new double [dataLength];
            foreach (MethodInfo method in methods)
            {
                if (method.Name == parames.InitialConditions) data = (double[])method.Invoke(null, new object[] { dataLength });
            }
            return data;
        }
        /*public static void Gnuplot(EquationParameters parames)
        {
            string pgm = @"C:\gnuplot\bin\gnuplot.exe";

            Process extPro = new Process();
            extPro.StartInfo.FileName = pgm;
            extPro.StartInfo.UseShellExecute = false;
            extPro.StartInfo.RedirectStandardInput = true;
            extPro.Start();
            StreamWriter gnupStWr = extPro.StandardInput;

            gnupStWr.WriteLine("reset");
            gnupStWr.WriteLine("cd 'C:/Users/mm/Documents/Visual Studio 2017/Projects/EquationsSolver/WebGUI/DataStorage/'");
            gnupStWr.WriteLine("set terminal gif animate");
            gnupStWr.WriteLine("set title \"{0}\"", parames.NumericalMethods);
            gnupStWr.WriteLine("set output 'result.gif'");
            gnupStWr.WriteLine("stats 'data.dat' nooutput");
            gnupStWr.WriteLine("set xrange [0:{0}]", parames.MaxVar);
            gnupStWr.WriteLine("set yrange [0:1.5]");
            gnupStWr.WriteLine("do for [i=1:int(STATS_blocks)] {");
            gnupStWr.WriteLine("plot 'data.dat' using 1:2 index(i-1) with lines title sprintf(\"t=%0.2f {1}\", i*{0})", parames.TimeStep.ToString("0.00",new CultureInfo("en-US")), parames.InitialConditions);
            gnupStWr.WriteLine("}");
            gnupStWr.Flush();
            gnupStWr.Close();
            extPro.Close();
            
        }*/
    }
}
