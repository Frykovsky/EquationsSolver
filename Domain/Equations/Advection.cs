using Domain.UsersDB;
using Domain.EquationsRelated;
using System;
using System.Text;

namespace Domain.Equations
{
    public static class Advection
    {
        public static string UpwindScheme(EquationParameters parameters)
        {
            double[] data = new double[(int) (parameters.MaxVar/parameters.VarStep)];
            data = EquationMethods.InitialConditionsSetter(parameters, data.Length);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < (int)(parameters.MaxTime / parameters.TimeStep); i++)
            {
                double[] reservoir = new double[data.Length];
                for (int j = 0; j < reservoir.Length; j++)
                {
                    reservoir[j] = data[j];
                }
                for (int j = 1; j < reservoir.Length; j++)
                {
                    data[j] = reservoir[j]-parameters.TimeStep*parameters.Coeffitient*(reservoir[j] - reservoir[j-1])/parameters.VarStep;
                }
                data[0] = reservoir[reservoir.Length - 1];
                result.Append(EquationMethods.FileWriter(data, parameters, i));
            }
            return result.ToString();
        }

        public static string SemiLagrangeScheme(EquationParameters parameters)
        {
            StringBuilder result = new StringBuilder();
            double[] data = new double[(int)(parameters.MaxVar / parameters.VarStep)];
            data = EquationMethods.InitialConditionsSetter(parameters, data.Length);
            for (int i = 0; i < (int)(parameters.MaxTime / parameters.TimeStep); i++)
            {
                double[] reservoir = new double[data.Length];
                double[] res = new double[data.Length];
                int approx;
                int secondapprox;
                for (int j = 0; j < reservoir.Length; j++)
                {
                    reservoir[j] = j-parameters.Coeffitient*parameters.TimeStep/parameters.VarStep;
                    res[j] = data[j];
                }
                for (int j = 1; j < reservoir.Length; j++)
                {
                    if (reservoir[j] > 0)
                    {
                        approx = (int)reservoir[j];
                        if (approx > reservoir[j]) secondapprox = approx - 1;
                        else if (approx < reservoir[j]) secondapprox = approx + 1;
			else secondapprox = approx;
                        data[j] = Math.Abs(approx - reservoir[j]) * res[approx] + Math.Abs(secondapprox - reservoir[j]) * res[secondapprox];
                    }
                    else
                    {
                        data[j] = data[0];
                    }
                }
                data[0] = res[reservoir.Length - 1];
                result.Append(EquationMethods.FileWriter(data, parameters, i));
            }
            return result.ToString();
        }
    }
}
