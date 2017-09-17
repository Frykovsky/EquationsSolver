using Domain.UsersDB;
using Domain.EquationsRelated;
using System.IO;
using System.Text;

namespace Domain.Equations
{
    public static class Diffusion
    {
        public static string CentralDifferences(EquationParameters parameters)
        {
            double[] data = new double[(int)(parameters.MaxVar / parameters.VarStep)];
            data = EquationMethods.InitialConditionsSetter(parameters, data.Length);
            StringBuilder result = new StringBuilder();
            //FileStream stream = new FileStream("C:/Users/mm/Documents/Visual Studio 2017/Projects/EquationsSolver/WebGUI/DataStorage/data.json", FileMode.Create, FileAccess.Write);
            for (int i = 0; i < (int)(parameters.MaxTime / parameters.TimeStep); i++)
            {
                double[] reservoir = new double[data.Length];
                for (int j = 0; j < reservoir.Length; j++)
                {
                    reservoir[j] = data[j];
                }
                for (int j = 1; j < reservoir.Length-1; j++)
                {
                    data[j] = reservoir[j] + parameters.TimeStep * parameters.Coeffitient * (reservoir[j+1]-2.0*reservoir[j] + reservoir[j - 1]) / (parameters.VarStep*parameters.VarStep);
                }
                result.Append(EquationMethods.FileWriter(data, parameters, i));
            }
            return result.ToString();
            //stream.Close();
        }
    }
}