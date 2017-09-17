using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain.EquationsRelated;

namespace Domain.UsersDB
{
    public class EquationParameters
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int EquationID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string EquationName { get; set; }

        [Required(ErrorMessage = "Set the initial conditions")]
        public string InitialConditions { get; set; }

        [Required(ErrorMessage = "Set the numerical method")]
        public string NumericalMethods { get; set; }

        [Range(0.01, 5, ErrorMessage = "MaxTime is not in range from 0.01 to 5")]
        public double MaxTime { get; set; }

        [Range(0.01, 5, ErrorMessage = "MaxVar is not in range from 0.01 to 5")]
        public double MaxVar { get; set; }

        [Range(0.01, 1, ErrorMessage = "TimeStep is not in range from 0.01 to 1")]
        public double TimeStep { get; set; }

        [Range(0.01, 1, ErrorMessage = "VarStep is not in range from 0.01 to 1")]
        public double VarStep { get; set; }

        [EquationCourant("VarStep", "TimeStep", "EquationName", "UseCourant")]
        [Range(0.0, double.MaxValue, ErrorMessage = "MaxTime is not in range")]
        public double Coeffitient { get; set; }

        public bool UseCourant { get; set; } = true;
    }
}
